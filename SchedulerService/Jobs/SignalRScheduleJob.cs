using Microsoft.AspNetCore.SignalR;
using Quartz;
using SchedulerService.Hubs;

namespace SchedulerService.Jobs;

public class SignalRScheduleJob : IJob
{
    private readonly IHubContext<SchedulerHub> _hubContext;

    private readonly ILogger _logger;

    public SignalRScheduleJob(IHubContext<SchedulerHub> hubContext, ILoggerFactory loggerFactory)
    {
        _hubContext = hubContext;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var jobKey = context.JobDetail.Key;
        _logger.LogInformation("执行任务[{}]", jobKey);

        var jobDetail = context.JobDetail;
        var connectionId = jobDetail.JobDataMap.GetString("ConnectionId");
        if (string.IsNullOrWhiteSpace(connectionId))
            throw new KeyNotFoundException("没有找到对应的connectionId");

        var methodName = jobDetail.JobDataMap.GetString("MethodName");
        if (string.IsNullOrWhiteSpace(methodName))
            throw new KeyNotFoundException("没有找到对应的回调方法");

        await _hubContext.Clients.Client(connectionId).SendAsync(methodName);
        _logger.LogInformation("{}", DateTimeOffset.Now);
    }
}