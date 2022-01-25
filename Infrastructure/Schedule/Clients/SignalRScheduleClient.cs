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

        private readonly IOptions<ScheduleOptions> _scheduleOptions;

        private readonly IOptionsMonitor<JobInfo> _jogInfoOptions;

        private readonly HubConnection _connection;

        private readonly IServiceProvider _services;

        private readonly Type _defaultJobExecutorType;

        private IDisposable _hubHandlerRegistries;

        public SignalRScheduleClient(
            ILoggerFactory loggerFactory, 
            IServiceProvider services,
            IOptions<ScheduleOptions> scheduleOptions, 
            IOptionsMonitor<JobInfo> jogInfoOptions)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _services = services;
            _scheduleOptions = scheduleOptions;
            _jogInfoOptions = jogInfoOptions;
            _defaultJobExecutorType = typeof(IJobExecutor<>);

            var clientOptions = scheduleOptions.Value.SignalRClientOptions;
            _connection = new HubConnectionBuilder()
                .WithUrl(clientOptions.Host)
                .WithAutomaticReconnect()
                .Build();
            _connection.HandshakeTimeout = clientOptions.HandShakeTimeout;
            _connection.KeepAliveInterval = clientOptions.KeepAliveInterval;
            _connection.ServerTimeout = clientOptions.ServerTimeout;
        }

        public virtual async Task StartAsync(CancellationToken token = default)
        {
            var scheduleOptions = _scheduleOptions.Value;
            foreach (var jobOptions in scheduleOptions.JobOptionsList)
            {
                _hubHandlerRegistries = RegisterJobExecutor(jobOptions);
            }
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

        public virtual IDisposable RegisterJobExecutor(JobOptions jobOptions)
        {
            var jobInfo = _jogInfoOptions.Get(jobOptions.Name);
            var jobExecutorType = _defaultJobExecutorType.MakeGenericType(jobOptions.ExecutorType);
            var jobExecutor = (IJobExecutor) _services.GetRequiredService(jobExecutorType);
            return _connection.On(jobInfo.MethodName, jobExecutor.ExecuteJobAsync);
        }

        public void Dispose()
        {
            _hubHandlerRegistries?.Dispose();
        }
    }
}