using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Schedule.JobExecutors
{
    public interface IJobExecutor : IDisposable
    {
        Task ExecuteJobAsync();
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

    // public interface IJobExecutor<T> : IJobExecutor where T : IJobExecutor
    // {
    //     IServiceProvider ServiceProvider { get; }
    //     
    //     ILogger Logger { get; }
    //     
    //     IJobExceptionHandler<T> ExceptionHandler { get; }
    //
    //     async Task IJobExecutor.ExecuteJobAsync()
    //     {
    //         var scope = ServiceProvider.CreateScope();
    //         try
    //         {
    //             var serviceProvider = scope.ServiceProvider;
    //             var jobExecutor = serviceProvider.GetRequiredService<T>();
    //             await jobExecutor.ExecuteJobAsync();
    //             
    //         }
    //         catch (Exception e)
    //         {
    //             ExceptionHandler.IncrementErrorExecuteCount();
    //             await ExceptionHandler.HandleExceptionAsync(e);
    //         }
    //         finally
    //         {
    //             ExceptionHandler.IncrementTotalExecuteCount();
    //             scope.Dispose();
    //         }
    //     }
    // }
}