using Infrastructure.Schedule.JobExecutors;

namespace SignalRSample.Services;

public class TestJobService : IJobExecutor
{
    private readonly ILogger _logger;

    public TestJobService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task ExecuteJobAsync()
    {
        _logger.LogInformation("模拟执行任务");
        _logger.LogInformation("this is {}", nameof(TestJobService));
        await Task.Delay(TimeSpan.FromSeconds(10));
        _logger.LogInformation("{}", DateTimeOffset.Now);
    }

    public void Dispose()
    {
        // throw new NotImplementedException();
    }
}