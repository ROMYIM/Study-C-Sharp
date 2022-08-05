using System.Data;
using FreeSql;
using FreeSqlSample.Common;
using FreeSqlSample.Models;
using FreeSqlSample.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FreeSqlSample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly CaseInfoRepository _caseInfoRepository;

    private readonly ProcInfoRepository _procInfoRepository;

    private readonly UnitOfWorkManagerCloud<string> _managerCloud;
    
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, 
        UnitOfWorkManagerCloud<string> managerCloud, CaseInfoRepository caseInfoRepository, ProcInfoRepository procInfoRepository)
    {
        _logger = logger;
        _managerCloud = managerCloud;
        _caseInfoRepository = caseInfoRepository;
        _procInfoRepository = procInfoRepository;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async ValueTask<IEnumerable<WeatherForecast>> Get()
    {
        using var unitOfWorks = _managerCloud.Begin("Default", "Test");

        var newCaseInfo = new CaseInfo()
        {
            CaseId = Guid.NewGuid().ToString()
        };
        _logger.LogDebug(newCaseInfo.CaseId);
        await _caseInfoRepository.InsertAsync(newCaseInfo);

        var newProcInfo = new CaseProcInfo()
        {
            CaseId = newCaseInfo.CaseId
        };
        _logger.LogDebug(newProcInfo.ProcId);
        await _procInfoRepository.InsertAsync(newProcInfo);

        unitOfWorks.Commit();
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}