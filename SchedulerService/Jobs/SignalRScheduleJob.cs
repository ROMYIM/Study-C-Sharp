using Microsoft.AspNetCore.SignalR;
using Quartz;
using SchedulerService.Hubs;

namespace SchedulerService.Jobs;

public class SignalRScheduleJob : IJob
{
    private readonly IHubContext<ServiceHub> _hubContext;

    private readonly ILogger _logger;

    public SignalRScheduleJob(IHubContext<ServiceHub> hubContext, ILoggerFactory loggerFactory)
    {
        _hubContext = hubContext;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var jobKey = context.JobDetail.Key;
        _logger.LogInformation("执行任务[{}]", jobKey);
        
        var connectionId = context.JobDetail.JobDataMap.GetString("ConnectionId");
        if (string.IsNullOrWhiteSpace(connectionId))
            throw new KeyNotFoundException("没有找到对应的connectionId");

        await _hubContext.Clients.Client(connectionId).SendAsync(nameof(Execute));
    }
}