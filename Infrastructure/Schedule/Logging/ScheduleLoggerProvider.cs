using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Infrastructure.Schedule.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.Logging
{
    public class ScheduleLoggerProvider : ILoggerProvider
    {

        private readonly ConcurrentDictionary<string, ScheduleLogger> _loggers;

        private readonly Channel<LogInfo> _logInfoChannel;

        private readonly IOptions<LoggerFilterOptions> _filterOptions;

        private ulong _isEnabled;

        public ScheduleLoggerProvider(IOptions<LoggerFilterOptions> filterOptions)
        {
            _filterOptions = filterOptions;
            _loggers = new ConcurrentDictionary<string, ScheduleLogger>();
            _logInfoChannel = Channel.CreateBounded<LogInfo>(1024);
        }

        public void Dispose()
        {
            _loggers.Clear();
            _logInfoChannel.Writer.TryComplete();
            _logInfoChannel.Reader.Completion.Wait();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new ScheduleLogger(this, name, _filterOptions.Value));
        }

        public bool IsEnabled => _isEnabled > 0;

        internal bool PushLogInfo<TScope>(LogInfo<TScope> logInfo)
        {
            return logInfo != null && _logInfoChannel.Writer.TryWrite(logInfo);
        }

        internal IAsyncEnumerable<LogInfo> TakeLogInfosAsync()
        {
            return _logInfoChannel.Reader.ReadAllAsync();

            // while (_logInfoChannel.Reader.TryRead(out var logInfo))
            // {
            //     yield return logInfo;
            // }
        }

        internal Task StartLoggingAsync()
        {
            Interlocked.Exchange(ref _isEnabled, 1);
            Console.WriteLine(nameof(StartLoggingAsync));
            return Task.CompletedTask;
        }

        internal Task StopLoggingAsync()
        {
            Interlocked.Exchange(ref _isEnabled, 0);
            Console.WriteLine(nameof(StopLoggingAsync));
            return Task.CompletedTask;
        }
    }
}