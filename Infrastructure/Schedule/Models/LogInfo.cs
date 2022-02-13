using System;
using System.Text;
using Infrastructure.Schedule.Extensions;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Schedule.Models
{
    public class LogInfo<TScope> : LogInfo
    {
        public TScope LogScope
        {
            get => (TScope) Scope;
            set => Scope = value;
        }
        
    }

    public abstract class LogInfo
    {
        public LogLevel Level { get; set; }

        public EventId EventId { get; set; }

        public string CategoryName { get; set; }

        public string LogContent { get; set; }

        internal object Scope { get; set; }

        public DateTime LogTime { get; set; }

        public override string ToString()
        {
            var logInfoBuilder = new StringBuilder();
            logInfoBuilder.Append(LogTime.ToString("yyyy-MM-dd HH:mm:ss")).Append(": ");
            logInfoBuilder.Append(Level.GetLogLevelString()).Append(": ");
            logInfoBuilder.Append(CategoryName);

            logInfoBuilder.Append('[').Append(EventId.Id);
            var scopeName = Scope?.ToString();
            if (!string.IsNullOrWhiteSpace(scopeName))
            {
                logInfoBuilder.Append(": ");
                logInfoBuilder.Append(scopeName);
            }

            logInfoBuilder.AppendLine("]");
            logInfoBuilder.AppendLine(LogContent);

            return logInfoBuilder.ToString();
        }
    }
}