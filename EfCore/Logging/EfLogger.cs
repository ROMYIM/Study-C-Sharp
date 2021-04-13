using System;
using Microsoft.Extensions.Logging;

namespace EfCore.Logging
{
    public class EfLogger : ILogger
    {
        private readonly string _categoryName;

        public EfLogger(string categoryName)
        {
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (_categoryName == "Microsoft.EntityFrameworkCore.Database.Command" && logLevel == LogLevel.Information)
            {
                var logContent = formatter(state, exception);
                System.Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                System.Console.WriteLine(logContent);
                Console.ResetColor();
            }
        }
    }
}