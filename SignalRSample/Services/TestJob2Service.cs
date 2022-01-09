using Infrastructure.Schedule.JobExecutors;

namespace SignalRSample.Services;

public class TestJob2Service : IJobExecutor
{
    private readonly ILogger<TestJob2Service> _logger;

    public TestJob2Service(ILogger<TestJob2Service> logger)
    {
        _logger = logger;
    }

    public Task ExecuteJobAsync()
    {
        _logger.LogInformation("this is {}", nameof(TestJob2Service));
        return Task.CompletedTask;
    }
}