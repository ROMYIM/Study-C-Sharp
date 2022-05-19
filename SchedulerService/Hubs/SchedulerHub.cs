using System.Diagnostics;
using Infrastructure.Schedule;
using Infrastructure.Schedule.Models;
using Microsoft.AspNetCore.SignalR;
using Quartz;
using SchedulerService.Jobs;

namespace SchedulerService.Hubs;

public class SchedulerHub : Hub
{
    public const string SchedulerName = "SignalrScheduler";

    private readonly ILogger _logger;

    private readonly SchedulerWorker _schedulerWorker;

    public SchedulerHub(ILoggerFactory loggerFactory, SchedulerWorker schedulerWorker)
    {
        _schedulerWorker = schedulerWorker;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task CreateJobAsync(JobInfo jobInfo)
    {
        using var logScope = _logger.BeginScope(Context.ConnectionId);
        var scheduler = await _schedulerWorker.GetSchedulerAsync(SchedulerName);

        var jobKey = JobKey.Create(jobInfo.JobKey);
        var jobDetail = await scheduler.GetJobDetail(jobKey);
        if (jobDetail == null)
        {
            var newJob = JobBuilder.Create<SignalRScheduleJob>()
                .WithIdentity(jobKey)
                .WithDescription(jobInfo.Description)
                .UsingJobData(nameof(Context.ConnectionId), Context.ConnectionId)
                .UsingJobData(nameof(jobInfo.MethodName), jobInfo.MethodName)
                .Build();

            var trigger = TriggerBuilder.Create()
                .ForJob(jobKey)
                .WithCronSchedule(jobInfo.CronExpression)
                .Build();

            await scheduler.ScheduleJob(newJob, trigger);
            if (Context.Items["JobKeys"] is ISet<JobKey> jobKeys)
                jobKeys.Add(jobKey);
            else
            {
                jobKeys = new HashSet<JobKey>();
                jobKeys.Add(jobKey);
                Context.Items["JobKeys"] = jobKeys;
            }
            _logger.LogInformation(1, "创建任务[{}]成功", jobKey);
            _logger.LogInformation(1, "调度策略:{}", jobInfo.CronExpression);
        }
    }
    
    public override Task OnConnectedAsync()
    {
        _logger.LogInformation(1, "Schedule Hub Connected: {}\nTime: {}", Context.ConnectionId, DateTime.Now);
        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        using var logScope = _logger.BeginScope(Context.ConnectionId);
        _logger.LogCritical(-1, "Schedule Hub Disconnected: {}\nTime: {}", Context.ConnectionId, DateTime.Now);
        
        if (Context.Items.TryGetValue("JobKeys", out var keys))
        {
            var scheduler = await _schedulerWorker.GetSchedulerAsync(SchedulerName);
            if (keys is ISet<JobKey> jobKeys)
            {
                foreach (var jobKey in jobKeys)
                {
                    var result = await scheduler.DeleteJob(jobKey);
                    Debug.Assert(result, "delete job failed");
                    Context.Items.Remove("JobKey");
                    _logger.LogInformation("移除任务[{}]。成功[{}]", jobKey, result);
                }
            }
        }
        await base.OnDisconnectedAsync(exception);
    }
    
}