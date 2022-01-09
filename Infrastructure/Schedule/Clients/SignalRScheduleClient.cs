using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Models;
using Infrastructure.Schedule.JobExecutors;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.Clients
{
    public class SignalRScheduleClient : IScheduleClient
    {
        private readonly ILogger _logger;

        private readonly IOptions<JobInfo> _options;

        private readonly HubConnection _connection;

        private readonly IServiceProvider _services;

        private IDisposable _hubHandlerRegistries;

        public SignalRScheduleClient(ILoggerFactory loggerFactory, IOptions<JobInfo> options, IServiceProvider services)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _options = options;
            _services = services;

            var jobInfoOptions = options.Value;
            _connection = new HubConnectionBuilder()
                .WithUrl(jobInfoOptions.Host)
                .WithAutomaticReconnect()
                .Build();
            
        }

        public virtual async Task StartAsync(CancellationToken token = default)
        {
            if (_connection.State == HubConnectionState.Disconnected)
            {
                await _connection.StartAsync(token);
                _logger.LogInformation("连接成功");
            }
        }

        public virtual async Task StopAsync(CancellationToken token = default)
        {
            if (_connection.State != HubConnectionState.Disconnected)
            {
                await _connection.StopAsync(token);
                _logger.LogInformation("断开连接");
            }
        }
        
        public virtual async Task CreateJobAsync(JobInfo jobInfo, CancellationToken token = default)
        {
            if (_connection.State == HubConnectionState.Disconnected)
            {
                await _connection.StartAsync(token);
                _logger.LogInformation("连接成功");
            }
            
            jobInfo ??= _options.Value;
            await _connection.SendAsync(nameof(CreateJobAsync), jobInfo, token);
            _logger.LogInformation("创建任务成功");
        }

        public virtual IDisposable RegisterJobExecutor(IJobExecutor jobExecutor) 
        {
            _hubHandlerRegistries = _connection.On(_options.Value.MethodName, jobExecutor.ExecuteJobAsync);
            return _hubHandlerRegistries;
        }

        public void Dispose()
        {
            _hubHandlerRegistries.Dispose();
        }
    }
}