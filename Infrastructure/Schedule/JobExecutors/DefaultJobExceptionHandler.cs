using Infrastructure.Schedule.Clients;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Schedule.JobExecutors
{
    public class DefaultJobExceptionHandler<T> : IJobExceptionHandler<T>
    {
        public IScheduleClient ScheduleClient { get; }
        
        public ILogger Logger { get; }

        public DefaultJobExceptionHandler(IScheduleClient scheduleClient, ILoggerFactory loggerFactory)
        {
            ScheduleClient = scheduleClient;
            Logger = loggerFactory.CreateLogger(typeof(T));
        }
    }
}