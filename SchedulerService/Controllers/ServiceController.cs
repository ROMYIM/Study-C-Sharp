using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SchedulerService.Hubs;

namespace SchedulerService.Controllers;

[ApiController]
[Route("services")]
public class ServiceController : ControllerBase
{
    private readonly ILogger _logger;

    private readonly IHubContext<SchedulerHub> _hubContext;


    public ServiceController(ILogger logger, IHubContext<SchedulerHub> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;
    }
    
    
}