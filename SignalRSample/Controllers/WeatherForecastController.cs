using Infrastructure.Schedule.JobExecutors;
using Microsoft.AspNetCore.Mvc;

namespace SignalRSample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly IEnumerable<IExceptionHandleJobExecutor> _jobExecutors;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IEnumerable<IExceptionHandleJobExecutor> jobExecutors)
    {
        _logger = logger;
        _jobExecutors = jobExecutors;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("total execute count {}", IExceptionHandleJobExecutor.TotalExecuteTimes);
        foreach (var jobExecutor in _jobExecutors)
        {
            _logger.LogInformation("[{}] total execute count {}", jobExecutor.JobExecutorType.Name, jobExecutor.JobTotalExecuteTimes);
        }

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}