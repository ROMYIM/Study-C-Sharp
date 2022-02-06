using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Schedule.Options;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.Logging
{
    public class ScheduleLoggerProvider : ILoggerProvider
    {
        private readonly HubConnection _connection;

        private readonly ConcurrentDictionary<string, ScheduleLogger> _loggers;

        private readonly Task _connectTask;

        private readonly BlockingCollection<string> _logs;

        private readonly IOptions<LoggerFilterOptions> _filterOptions;

        private ulong _isEnabled;

        public ScheduleLoggerProvider(IOptions<ScheduleOptions> scheduleOptions, IOptions<LoggerFilterOptions> filterOptions)
        {
            _filterOptions = filterOptions;
            _loggers = new ConcurrentDictionary<string, ScheduleLogger>();
            _logs = new BlockingCollection<string>(1024);
            
            var clientOptions = scheduleOptions.Value.SignalRClientOptions;
            _connection = new HubConnectionBuilder()
                .WithUrl($"{clientOptions.Host}/logs")
                .WithAutomaticReconnect()
                .Build();
            _connection.HandshakeTimeout = clientOptions.HandShakeTimeout;
            _connection.KeepAliveInterval = clientOptions.KeepAliveInterval;
            _connection.ServerTimeout = clientOptions.ServerTimeout;

            _connection.On(nameof(StartLoggingAsync), StartLoggingAsync);
            _connection.On(nameof(StopLoggingAsync), StopLoggingAsync);
            
            _connectTask = _connection.StartAsync();
            _connectTask.Start();

            PostLogsAsync();
        }

        public void Dispose()
        {
            if (_connection == null) return;
            
            if (_connection.State == HubConnectionState.Connected)
                _connection.StopAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                
            _connection.DisposeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (_loggers.TryGetValue(categoryName, out var logger))
                return logger;
            logger = new ScheduleLogger(this, categoryName, _filterOptions.Value);
            return _loggers.GetOrAdd(categoryName, logger);
        }

        public bool IsEnabled
        {
            get
            {
                if (_connectTask.IsCompletedSuccessfully)
                {
                    // _connectTask.ConfigureAwait(false).GetAwaiter().GetResult();
                    return _isEnabled > 0;
                }
                return false;
            }
        }

        internal bool PushLogInfo(string logInfo)
        {
            return !string.IsNullOrWhiteSpace(logInfo) && _logs.TryAdd(logInfo);
        }

        private Task StartLoggingAsync()
        {
            Interlocked.Exchange(ref _isEnabled, 1);
            return Task.CompletedTask;
        }

        private Task StopLoggingAsync()
        {
            Interlocked.Exchange(ref _isEnabled, 0);
            return Task.CompletedTask;
        }

        private Task PostLogsAsync()
        {
            return Task.Factory.StartNew(async loggerProvider =>
            {
                var scheduleLoggerProvider = (ScheduleLoggerProvider) loggerProvider;
                while (scheduleLoggerProvider.IsEnabled && scheduleLoggerProvider._logs.TryTake(out var logContent))
                {

                    await _connection.SendAsync(nameof(PostLogsAsync), logContent);
                }
            },  this, TaskCreationOptions.LongRunning);
        }
    }
}