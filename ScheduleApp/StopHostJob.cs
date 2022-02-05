using Infrastructure.Schedule.JobExecutors;

namespace ScheduleApp;

public class StopHostJob : IJobExecutor
{
    private readonly IHost _host;

    private readonly ILogger _logger;

    public StopHostJob(IHost host, ILoggerFactory loggerFactory)
    {
        _host = host;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task ExecuteJobAsync()
    {
        _logger.LogInformation("结束进程");
        await _host.StopAsync();
    }

    public void Dispose()
    {
        
    }
}