using Microsoft.AspNetCore.Mvc;
using SchedulerService.Models;
using SchedulerService.Query;
using SchedulerService.Repositories;

namespace SchedulerService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HostController : ControllerBase
{
    private readonly ServiceHostRepository _serviceHostRepository;
    private readonly ServiceHostQueryService _query;

    public HostController(ServiceHostRepository serviceHostRepository, ServiceHostQueryService query)
    {
        _serviceHostRepository = serviceHostRepository;
        _query = query;
    }

    [HttpGet("")]
    public IEnumerable<ServiceHost?> AllHost()
    {
        return _query.GetAllServiceHost();
    }
}