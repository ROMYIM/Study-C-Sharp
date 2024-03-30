using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Schedule.JobExecutors;
using Infrastructure.Schedule.Models;
using Infrastructure.Schedule.Options;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.Clients
{
    internal sealed class SignalRScheduleClient : IScheduleClient
    {
        private readonly ILogger _logger;

        private readonly ISignalRClient _client;

        private readonly IOptions<ScheduleOptions> _scheduleOptions;

        private readonly IServiceProvider _services;

        private readonly Type _defaultJobExecutorType;

        private IDisposable _hubHandlerRegistries;

        public SignalRScheduleClient(
            ILoggerFactory loggerFactory, 
            IServiceProvider services,
            IOptions<ScheduleOptions> scheduleOptions, 
            SignalRClientFactory clientFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _services = services;
            _scheduleOptions = scheduleOptions;
            _client = clientFactory.CreateClient(IScheduleClient.HubName);
            _defaultJobExecutorType = typeof(IJobExecutor<>);
            
        }

        public HubConnection Connection => _client.Connection;

        public async Task StartAsync(CancellationToken token = default)
        {
            var scheduleOptions = _scheduleOptions.Value;
            foreach (var jobOptions in scheduleOptions.JobOptionsMap.Values)
            {
                _hubHandlerRegistries = RegisterJobExecutor(jobOptions);
            }
            if (Connection.State == HubConnectionState.Disconnected)
            {
                await Connection.StartAsync(token);
                _logger.LogInformation("连接成功");
            }
        }

        public async Task StopAsync(CancellationToken token = default)
        {
            if (Connection.State != HubConnectionState.Disconnected)
            {
                await Connection.StopAsync(token);
                _logger.LogInformation("断开连接");
            }
        }
        
        public async Task CreateJobAsync(JobInfo jobInfo, CancellationToken token = default)
        {
            if (Connection.State == HubConnectionState.Disconnected)
            {
                await Connection.StartAsync(token);
                _logger.LogInformation("连接成功");
            }
            
            await Connection.SendAsync(nameof(CreateJobAsync), jobInfo, token);
            _logger.LogInformation("创建任务成功");
        }

        public async Task ReturnResultAsync(JobExecuteResult jobResult, CancellationToken token)
        {
            if (Connection.State == HubConnectionState.Connected)
            {
                await Connection.SendAsync(nameof(ReturnResultAsync), jobResult, token);
                _logger.LogInformation("任务结果回传");
            }
            
            _logger.LogCritical("连接断开。无法回传结果");
        }

        private IDisposable RegisterJobExecutor(JobOptions jobOptions)
        {
            var jobExecutorType = _defaultJobExecutorType.MakeGenericType(jobOptions.ExecutorType);
            var jobExecutor = (IJobExecutor) _services.GetRequiredService(jobExecutorType);
            return Connection.On(jobOptions.JobInfo.MethodName, jobExecutor.ExecuteJobAsync);
        }

        public ValueTask DisposeAsync()
        {
            _hubHandlerRegistries?.Dispose();
            return _client.DisposeAsync();
        }
    }
}