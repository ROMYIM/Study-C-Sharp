using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SchedulerService.Hubs;

namespace SchedulerService.Controllers;

[ApiController]
[Route("[controller]")]
public class LogsController : ControllerBase
{
    private readonly IHubContext<LoggingHub> _hubContext;

    private readonly ILogger<LogsController> _logger;

    public LogsController(IHubContext<LoggingHub> hubContext, ILogger<LogsController> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    [HttpGet]
    public string Index() => "logs";

    [HttpPost("start")]
    public async Task<string> StartLoggingAsync()
    {
        await _hubContext.Clients.All.SendAsync(nameof(StartLoggingAsync));
        _logger.LogInformation(nameof(StartLoggingAsync));
        return "start logging completed";
    }

    [HttpPost("stop")]
    public async Task<string> StopLoggingAsync()
    {
        await _hubContext.Clients.All.SendAsync(nameof(StopLoggingAsync));
        _logger.LogInformation(nameof(StopLoggingAsync));
        return "stop logging completed";
    }
}