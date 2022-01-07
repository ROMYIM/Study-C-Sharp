using Quartz;

namespace SchedulerService;

public class SchedulerWorker : BackgroundService
{
    private readonly ISchedulerFactory _schedulerFactory;

    private readonly ILogger _logger;

    public SchedulerWorker(ISchedulerFactory schedulerFactory, ILoggerFactory loggerFactory)
    {
        _schedulerFactory = schedulerFactory;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public virtual async Task<IScheduler> GetSchedulerAsync(string? schedulerName = null)
    {
        if (string.IsNullOrWhiteSpace(schedulerName))
            return await _schedulerFactory.GetScheduler();
        return await _schedulerFactory.GetScheduler(schedulerName) ?? await _schedulerFactory.GetScheduler();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var schedulers = await _schedulerFactory.GetAllSchedulers(stoppingToken);
        if (schedulers.Count == 0)
        {
            _logger.LogInformation("创建默认调度器");
            schedulers = new[] {await _schedulerFactory.GetScheduler(stoppingToken)};
        }
        
        foreach (var scheduler in schedulers)
        {
            await scheduler.Start(stoppingToken);
            _logger.LogInformation("调度器{}启动", scheduler.SchedulerName);
        }
    }
}