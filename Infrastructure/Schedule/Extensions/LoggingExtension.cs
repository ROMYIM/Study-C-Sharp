using System;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Schedule.Extensions
{
    public static class LoggingExtension
    {
        public static string GetLogLevelString(this LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => "trce",
                LogLevel.Debug => "dbug",
                LogLevel.Information => "info",
                LogLevel.Warning => "warn",
                LogLevel.Error => "fail",
                LogLevel.Critical => "crit",
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel))
            };
        }
    }
}