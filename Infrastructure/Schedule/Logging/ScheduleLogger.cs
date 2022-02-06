using System;
using System.Linq;
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