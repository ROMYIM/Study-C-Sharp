using System.Diagnostics;
using Infrastructure.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Quartz;
using SchedulerService.Jobs;

namespace SchedulerService.Hubs;

public class ServiceHub : Hub
{
    private readonly Guid _instanceId;

    private readonly ILogger _logger;

    private readonly ISchedulerFactory _schedulerFactory;

    private readonly IOptions<IEnumerable<SchedulerInfo>> _schedulerInfoOptions;

    public const string ScheduleName = "SignalRScheduler";

    public ServiceHub(ILoggerFactory loggerFactory, 
        ISchedulerFactory schedulerFactory, 
        IOptions<IEnumerable<SchedulerInfo>> schedulerInfoOptions)
    {
        _schedulerFactory = schedulerFactory;
        _schedulerInfoOptions = schedulerInfoOptions;
        _logger = loggerFactory.CreateLogger(GetType());
        _instanceId = Guid.NewGuid();
        _logger.LogInformation("{}", _instanceId);
    }

    public async Task CreateJobAsync(JobInfo jobInfo)
    {
        var scheduler = await _schedulerFactory.GetScheduler(ScheduleName);
        if (scheduler != null)
        {
            var jobKey = JobKey.Create(jobInfo.JobKey);
            var jobDetail = await scheduler.GetJobDetail(jobKey);
            if (jobDetail == null)
            {
                var newJob = JobBuilder.Create<SignalRScheduleJob>()
                    .WithIdentity(jobKey)
                    .WithDescription(jobInfo.Description)
                    .UsingJobData(nameof(Context.ConnectionId), Context.ConnectionId)
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .ForJob(jobKey)
                    .WithCronSchedule(jobInfo.CronExpression)
                    .Build();

                await scheduler.ScheduleJob(newJob, trigger);
                Context.Items["JobKey"] = jobKey;
            }
        }

        throw new NullReferenceException(nameof(scheduler));
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("连接[{}]断开", Context.ConnectionId);
        
        if (Context.Items.TryGetValue("JobKey", out var jobKeyObject))
        {
            var jobKey = (JobKey) jobKeyObject!;
            var scheduler = await _schedulerFactory.GetScheduler(ScheduleName);
            if (scheduler != null)
            {
                var result = await scheduler.DeleteJob(jobKey);
                Debug.Assert(result, "delete job failed");
                Context.Items.Remove("JobKey");
                _logger.LogInformation("移除任务[{}]", jobKey);
            }
        }
        await base.OnDisconnectedAsync(exception);
    }
    
}