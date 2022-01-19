using System;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Schedule.JobExecutors
{
    public class DefaultJobExecutor<T> : IJobExecutor<T> where T : IJobExecutor
    {
        public IServiceProvider Services { get; }
        
        public ILogger Logger { get; }

        public DefaultJobExecutor(IServiceProvider services, ILoggerFactory loggerFactory)
        {
            Services = services;
            Logger = loggerFactory.CreateLogger(nameof(T));
        }
    }
}