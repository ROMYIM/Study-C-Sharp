using Infrastructure.Models;
using Microsoft.Extensions.Options;
using Quartz;

namespace SchedulerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    private readonly ISchedulerFactory _schedulerFactory;

    private readonly IOptionsMonitor<FilesDeleteJobOptions> _optionsMonitor;

    private readonly IOptions<QuartzOptions> _options;

    private IDisposable? _optionsChangeRegistries;

    private IScheduler? _scheduler;

    public static string CronExpressionKey(string jobKey) => $"{jobKey}.Cron";

    public Worker(ILogger<Worker> logger, ISchedulerFactory schedulerFactory, IOptionsMonitor<FilesDeleteJobOptions> optionsMonitor, IOptions<QuartzOptions> options)
    {
        _logger = logger;
        _schedulerFactory = schedulerFactory;
        _optionsMonitor = optionsMonitor;
        _options = options;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _optionsChangeRegistries = _optionsMonitor.OnChange(async options =>
        {
            _logger.LogInformation("配置文件修改");
            var refreshResult = await RefreshJobTriggerAsync(options, cancellationToken);
            if (refreshResult)
                _logger.LogInformation("触发器刷新成功");
            else
                _logger.LogError("触发器刷新失败");
        });
        return base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _scheduler?.Shutdown(true, cancellationToken)!;
        await base.StopAsync(cancellationToken);
    }

    public override void Dispose()
    {
        base.Dispose();
        _optionsChangeRegistries?.Dispose();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _scheduler = await _schedulerFactory.GetScheduler(cancellationToken: stoppingToken);
        await _scheduler.Start(cancellationToken: stoppingToken);
    }

    private async Task<bool> RefreshJobTriggerAsync(FilesDeleteJobOptions newOptions, CancellationToken cancellationToken)
    {
        if (_scheduler != null)
        {
            var jobKey = JobKey.Create(newOptions.JobKey);
            var triggers = await _scheduler.GetTriggersOfJob(jobKey, cancellationToken);
            if (triggers.Count > 1)
            {
                _logger.LogError("触发器大于一个");
                return false;
            }
            else
            {
                var originalTrigger = triggers.Single();
                var quartzOptions = _options.Value;
                var cronExpression = quartzOptions[CronExpressionKey(jobKey.Name)];
                if (cronExpression != newOptions.CronExpression)
                {
                    var triggerKey = originalTrigger.Key;
                    var newTrigger = TriggerBuilder.Create().ForJob(jobKey).WithIdentity(triggerKey)
                        .WithCronSchedule(newOptions.CronExpression).Build();
                    await _scheduler.RescheduleJob(triggerKey, newTrigger, cancellationToken);
                    quartzOptions[CronExpressionKey(jobKey.Name)] = newOptions.CronExpression;
                    return true;
                }
            }
        }

        return false;
    }
}