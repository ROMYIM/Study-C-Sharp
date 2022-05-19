using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Infrastructure.Schedule.BackgroundServices;
using Infrastructure.Schedule.Clients;
using Infrastructure.Schedule.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Schedule.Logging
{
    public class ScheduleLogger : ILogger
    {
        private readonly ScheduleLoggerProvider _loggerProvider;

        private readonly string _categoryName;

        private readonly LoggerFilterOptions _filterOptions;

        private ScheduleLogScope _logScope;

        private static readonly ImmutableHashSet<string> ExcludesCategories = new HashSet<string>()
        {
            typeof(ScheduleLoggerProvider).FullName,
            typeof(SignalRLoggingClient).FullName,
            typeof(SignalRLoggingWorker).FullName
        }.ToImmutableHashSet();

        public ScheduleLogger(ScheduleLoggerProvider loggerProvider, string categoryName, LoggerFilterOptions filterOptions)
        {
            _loggerProvider = loggerProvider;
            _categoryName = categoryName;
            _filterOptions = filterOptions;
            _logScope = new ScheduleLogScope();
        }

        #nullable enable
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            if (state == null) throw new ArgumentNullException(nameof(state));
            formatter = FormatMessage;

            var logContent = FormatLogContent(state, exception);

            var logInfo = new LogInfo<ScheduleLogScope>()
            {
                Level = logLevel,
                EventId = eventId,
                CategoryName = _categoryName,
                LogContent = logContent,
                LogScope = _logScope,
                LogTime = DateTime.Now
            };

            _loggerProvider.PushLogInfo(logInfo);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            if (!_loggerProvider.IsEnabled) return false;
            if (ExcludesCategories.Contains(_categoryName)) return false;
            
            var rule = _filterOptions.Rules.FirstOrDefault(r =>
                string.Equals(_categoryName, r.CategoryName, StringComparison.OrdinalIgnoreCase));
            var minLevel = rule?.LogLevel ?? _filterOptions.MinLevel;
            return logLevel >= minLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            _logScope = new ScheduleLogScope(state);
            return _logScope;
        }

        #nullable enable
        private string FormatLogContent<TState>(TState state, Exception? exception)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            var contentBuilder = new StringBuilder();
            contentBuilder.AppendLine(state.ToString());

            if (exception != null)
            {
                contentBuilder.AppendLine(exception.Message);
                contentBuilder.AppendLine(exception.StackTrace);
            }

            return contentBuilder.ToString();
        }

        private string FormatMessage<TState>(TState state, Exception? exception)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));

            var formattedMessageBuilder = new StringBuilder();
            formattedMessageBuilder.AppendLine(state.ToString());

            if (exception != null)
            {
                formattedMessageBuilder.Append("error: ").AppendLine(exception.Message);
                formattedMessageBuilder.Append("source: ").AppendLine(exception.Source);
                formattedMessageBuilder.Append("stack trace: ").AppendLine(exception.StackTrace);
            }

            return formattedMessageBuilder.ToString();;
        }
    }
}