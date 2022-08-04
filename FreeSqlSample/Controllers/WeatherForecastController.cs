using System.Data;
using FreeSql;
using FreeSqlSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace FreeSqlSample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly FreeSqlCloud<string> _freeSql;

    private readonly UnitOfWorkManager _unitOfWorkManager;
    
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, FreeSqlCloud<string> freeSql, UnitOfWorkManager unitOfWorkManager)
    {
        _logger = logger;
        _freeSql = freeSql;
        _unitOfWorkManager = unitOfWorkManager;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        using var unitOfWork = _unitOfWorkManager.Begin(isolationLevel:IsolationLevel.ReadCommitted);
        using var transaction = unitOfWork.GetOrBeginTransaction();
        
        var newCaseInfo = new CaseInfo()
        {
            CaseId = Guid.NewGuid().ToString()
        };
        
        _logger.LogDebug(newCaseInfo.CaseId);

        _freeSql.Insert(newCaseInfo).WithTransaction(transaction).ExecuteAffrows();
        _freeSql.Change("Test").Insert(newCaseInfo).WithTransaction(transaction).ExecuteAffrows();

        unitOfWork.Commit();
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}