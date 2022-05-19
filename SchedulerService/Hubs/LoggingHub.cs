using Infrastructure.Schedule.Logging;
using Infrastructure.Schedule.Models;
using Microsoft.AspNetCore.SignalR;

namespace SchedulerService.Hubs;

public class LoggingHub : Hub
{
    private readonly ILogger<LoggingHub> _logger;

    public LoggingHub(ILogger<LoggingHub> logger)
    {
        _logger = logger;
    }

    public Task PostLogsAsync(LogInfo<ScheduleLogScope>[] logs)
    {
        foreach (var logInfo in logs)
        {
            Console.WriteLine(logInfo.ToString());
        }
        return Task.CompletedTask;
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation(1, "Logging Hub Connected: {}\nTime: {}", Context.ConnectionId, DateTime.Now);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogCritical(-1, "Logging Hub Disconnected: {}\nTime: {}", Context.ConnectionId, DateTime.Now);
        return base.OnDisconnectedAsync(exception);
    }
}