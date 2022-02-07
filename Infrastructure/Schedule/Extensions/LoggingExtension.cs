using System;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Schedule.Extensions
{
    public static class LoggingExtension
    {
        public static string GetLogLevelString(this LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return "trce";
                case LogLevel.Debug:
                    return "dbug";
                case LogLevel.Information:
                    return "info";
                case LogLevel.Warning:
                    return "warn";
                case LogLevel.Error:
                    return "fail";
                case LogLevel.Critical:
                    return "crit";
                default:
                    throw new ArgumentOutOfRangeException(nameof (logLevel));
            }
        }
    }
}