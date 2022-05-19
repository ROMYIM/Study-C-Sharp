using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Models;
using Infrastructure.Schedule.Clients;
using Infrastructure.Schedule.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Schedule.JobExecutors
{
    public interface IJobExceptionHandler
    {
        IScheduleClient ScheduleClient { get; }
        
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

        Func<Exception, JobExecuteResult> GenerateErrorResult => e => new JobExecuteResult()
        {
            Message = e.Message,
            ExecuteTime = DateTimeOffset.Now,
            Result = JobResult.Failed
        };
    }

    public interface IJobExceptionHandler<T> : IJobExceptionHandler
    {
        private static ulong _totalExecuteTimes;

        private static ulong _totalErrorTimes;

        ulong IJobExceptionHandler.JobTotalExecuteTimes => _totalExecuteTimes;

        ulong IJobExceptionHandler.JobTotalErrorTimes => _totalErrorTimes;

        Type IJobExceptionHandler.JobExecutorType => typeof(T);
        
        async Task HandleExceptionAsync(Exception exception)
        {
            Logger.LogError("Job [{}] throw error: {} \r\n{}", nameof(T), exception.Message, exception.StackTrace);
            var result = GenerateErrorResult(exception);
            await ScheduleClient.ReturnResultAsync(result, default);
        }

        new void IncrementTotalExecuteCount()
        {
            ((IJobExceptionHandler)this).IncrementTotalExecuteCount();
            Interlocked.Increment(ref _totalExecuteTimes);
        }

        new void IncrementErrorExecuteCount()
        {
            ((IJobExceptionHandler)this).IncrementErrorExecuteCount();
            Interlocked.Increment(ref _totalErrorTimes);
        }
    }
}