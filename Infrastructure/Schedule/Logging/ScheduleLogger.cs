using System;
using System.Linq;
using System.Text;
using Infrastructure.Schedule.Extensions;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Schedule.Logging
{
    public class ScheduleLogger : ILogger
    {
        private readonly ScheduleLoggerProvider _loggerProvider;

        private readonly string _categoryName;

        private readonly LoggerFilterOptions _filterOptions;

        private ScheduleLogScope _logScope;

        public ScheduleLogger(ScheduleLoggerProvider loggerProvider, string categoryName, LoggerFilterOptions filterOptions)
        {
            _loggerProvider = loggerProvider;
            _categoryName = categoryName;
            _filterOptions = filterOptions;
            _logScope = new ScheduleLogScope();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            if (state == null) throw new ArgumentNullException(nameof(state));
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));

            var logContent = formatter(state, exception);
            
            var logInfoBuilder = new StringBuilder();
            logInfoBuilder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append(": ");
            logInfoBuilder.Append(logLevel.GetLogLevelString()).Append(": ");
            logInfoBuilder.Append(_categoryName);

            logInfoBuilder.Append('[').Append(eventId.Id);
            var scopeName = _logScope.ToString();
            if (!string.IsNullOrWhiteSpace(scopeName))
            {
                logInfoBuilder.Append(": ");
                logInfoBuilder.Append(scopeName);
            }

            logInfoBuilder.AppendLine("]");
            logInfoBuilder.AppendLine(logContent);

            var logInfo = logInfoBuilder.ToString();
            _loggerProvider.PushLogInfo(logInfo);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            if (!_loggerProvider.IsEnabled) return false;
            
            var rule = _filterOptions.Rules.FirstOrDefault(r =>
                string.Equals(_categoryName, r.CategoryName, StringComparison.OrdinalIgnoreCase));
            var minLevel = rule?.LogLevel ?? _filterOptions.MinLevel;
            return logLevel > minLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            _logScope = new ScheduleLogScope(state);
            return _logScope;
        }
    }
}