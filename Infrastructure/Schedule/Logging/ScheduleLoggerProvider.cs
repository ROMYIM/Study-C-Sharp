using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Schedule.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.Logging
{
    public class ScheduleLoggerProvider : ILoggerProvider
    {

        private readonly ConcurrentDictionary<string, ScheduleLogger> _loggers;

        private readonly BlockingCollection<LogInfo> _logs;

        private readonly IOptions<LoggerFilterOptions> _filterOptions;

        private ulong _isEnabled;

        public ScheduleLoggerProvider(IOptions<LoggerFilterOptions> filterOptions)
        {
            _filterOptions = filterOptions;
            _loggers = new ConcurrentDictionary<string, ScheduleLogger>();
            _logs = new BlockingCollection<LogInfo>(1024);
        }

        public void Dispose()
        {
            _loggers.Clear();
            _logs.Dispose();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new ScheduleLogger(this, name, _filterOptions.Value));
        }

        public bool IsEnabled => _isEnabled > 0;

        internal bool PushLogInfo<TScope>(LogInfo<TScope> logInfo)
        {
            return logInfo != null && _logs.TryAdd(logInfo);
        }

        internal IEnumerable<LogInfo> TakeLogInfos(TimeSpan timeout)
        {
            while (_logs.TryTake(out var logInfo, timeout))
            {
                yield return logInfo;
            }
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