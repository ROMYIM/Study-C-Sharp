using Infrastructure.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace SignalRSample;

public class HubWorker : BackgroundService
{
    private readonly ILogger _logger;

    private readonly HubConnection _connection;

    private readonly IOptions<JobInfo> _options;

    public HubWorker(ILoggerFactory loggerFactory, IOptions<JobInfo> options)
    {
        _options = options;
        _logger = loggerFactory.CreateLogger(GetType());
        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5020/services")
            .Build();
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _connection.KeepAliveInterval = Timeout.InfiniteTimeSpan;
        _connection.On("Execute", () => _logger.LogInformation("模拟执行任务"));
        await _connection.StartAsync(cancellationToken);
        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _connection.InvokeAsync("CreateJobAsync", _options.Value, cancellationToken: stoppingToken);
       
    }
}