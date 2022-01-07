using System.Diagnostics;
using Infrastructure.Models;
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
            Context.Items["JobKey"] = jobKey;
            _logger.LogInformation("创建任务成功");
            _logger.LogInformation("调度策略:{}", jobInfo.CronExpression);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("连接[{}]断开", Context.ConnectionId);
        
        if (Context.Items.TryGetValue("JobKey", out var jobKeyObject))
        {
            var scheduler = await _schedulerWorker.GetSchedulerAsync(SchedulerName);
            var jobKey = (JobKey) jobKeyObject!;
            var result = await scheduler.DeleteJob(jobKey);
            Debug.Assert(result, "delete job failed");
            Context.Items.Remove("JobKey");
            _logger.LogInformation("移除任务[{}]", jobKey);
        }
        await base.OnDisconnectedAsync(exception);
    }
    
}