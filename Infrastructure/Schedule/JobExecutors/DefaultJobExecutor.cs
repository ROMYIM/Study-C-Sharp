using System;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Schedule.JobExecutors
{
    public class DefaultJobExecutor<T> : IExceptionHandleJobExecutor<T> where T : IJobExecutor
    {
        public IServiceProvider ServiceProvider { get; }
        
        public ILogger Logger { get; }

        public DefaultJobExecutor(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            ServiceProvider = serviceProvider;
            Logger = loggerFactory.CreateLogger(typeof(IExceptionHandleJobExecutor));
        }
    }
}