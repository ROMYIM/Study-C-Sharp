using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Schedule.JobExecutors
{
    public interface IExceptionHandleJobExecutor<T> : IExceptionHandleJobExecutor where T : IJobExecutor
    {

        private static ulong _totalExecuteTimes;

        private static ulong _totalErrorTimes;

        ulong IExceptionHandleJobExecutor.JobTotalExecuteTimes => _totalExecuteTimes;

        ulong IExceptionHandleJobExecutor.JobTotalErrorTimes => _totalErrorTimes;

        Type IExceptionHandleJobExecutor.JobExecutorType => typeof(T);

        new void IncrementTotalExecuteCount()
        {
            ((IExceptionHandleJobExecutor)this).IncrementTotalExecuteCount();
            Interlocked.Increment(ref _totalExecuteTimes);
        }

        new void IncrementErrorExecuteCount()
        {
            ((IExceptionHandleJobExecutor)this).IncrementErrorExecuteCount();
            Interlocked.Increment(ref _totalErrorTimes);
        }
        
        async Task IJobExecutor.ExecuteJobAsync()
        {
            var scope = ServiceProvider.CreateScope();
            try
            {
                var serviceProvider = scope.ServiceProvider;
                var jobExecutor = serviceProvider.GetRequiredService<T>();
                await jobExecutor.ExecuteJobAsync();
                
            }
            catch (Exception e)
            {
                Logger.LogError(e, "[{}] throws error", nameof(T));
                IncrementErrorExecuteCount();
            }
            finally
            {
                IncrementTotalExecuteCount();
                scope.Dispose();
            }
        }
    }

    public interface IExceptionHandleJobExecutor : IJobExecutor
    {
        IServiceProvider ServiceProvider { get; }
        
        ILogger Logger { get; }
        
        Type JobExecutorType { get; }
        
        private static ulong _totalExecuteTimes;

        private static ulong _totalErrorTimes;

        static ulong TotalExecuteTimes => _totalExecuteTimes;

        static ulong TotalErrorTimes => _totalErrorTimes;
        
        ulong JobTotalExecuteTimes { get; }
        
        ulong JobTotalErrorTimes { get; }

        void IncrementTotalExecuteCount() => Interlocked.Increment(ref _totalExecuteTimes);

        void IncrementErrorExecuteCount() => Interlocked.Increment(ref _totalErrorTimes);
        
    }
}