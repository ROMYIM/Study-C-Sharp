using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Schedule.JobExecutors
{
    public interface IJobExecutor
    {
        public Task ExecuteJobAsync();
    }

    public interface IJobExecutor<T> : IJobExecutor where T : IJobExecutor
    {
        IServiceProvider Services { get; }
        
        ILogger Logger { get; }

        async Task IJobExecutor.ExecuteJobAsync()
        {
            var scope = Services.CreateScope();
            try
            {
                var services = scope.ServiceProvider;
                var jobExecutor = services.GetRequiredService<T>();
                await jobExecutor.ExecuteJobAsync();
            }
            finally
            {
                scope.Dispose();
            }
        }
    }
}