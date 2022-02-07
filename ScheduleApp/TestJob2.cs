using Infrastructure.Schedule.JobExecutors;

namespace ScheduleApp;

public class TestJob2 : IJobExecutor
{
    private readonly ILogger<TestJob2> _logger;

    public TestJob2(ILogger<TestJob2> logger)
    {
        _logger = logger;
    }

    public Task ExecuteJobAsync()
    {
        _logger.LogInformation("第二个模拟任务测试");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        // throw new NotImplementedException();
    }
}