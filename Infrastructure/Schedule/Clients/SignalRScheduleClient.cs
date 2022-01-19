using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Schedule.JobExecutors;
using Infrastructure.Schedule.Options;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.Clients
{
    public class SignalRScheduleClient : IScheduleClient
    {
        private readonly ILogger _logger;

        private readonly IOptions<SignalRClientOptions> _clientOptions;

        private readonly HubConnection _connection;

        private readonly IServiceProvider _services;

        private IDisposable _hubHandlerRegistries;

        public SignalRScheduleClient(
            ILoggerFactory loggerFactory, 
            IOptions<SignalRClientOptions> clientOptions,
            IServiceProvider services)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _clientOptions = clientOptions;
            _services = services;

            var clientConnectOptions = _clientOptions.Value;
            _connection = new HubConnectionBuilder()
                .WithUrl(clientConnectOptions.Host)
                .WithAutomaticReconnect()
                .Build();
            _connection.HandshakeTimeout = clientConnectOptions.HandShakeTimeout;
            _connection.KeepAliveInterval = clientConnectOptions.KeepAliveInterval;
            _connection.ServerTimeout = clientConnectOptions.ServerTimeout;
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
            
            await _connection.SendAsync(nameof(CreateJobAsync), jobInfo, token);
            _logger.LogInformation("创建任务成功");
        }

        public virtual IDisposable RegisterJobExecutor<T>(JobInfo jobInfo) where T : IJobExecutor
        {
            var jobExecutor = _services.GetRequiredService<T>();
            _hubHandlerRegistries = _connection.On(jobInfo.MethodName, jobExecutor.ExecuteJobAsync);
            return _hubHandlerRegistries;
        }

        public void Dispose()
        {
            _hubHandlerRegistries.Dispose();
        }
    }
}