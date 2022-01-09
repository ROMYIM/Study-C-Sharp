using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Schedule.JobExecutors
{
    public interface IConcurrentJobExecutor<T> : IExceptionHandleJobExecutor<T> where T : IJobExecutor
    {
        internal uint ConcurrentCount { get; }

        internal static uint RunningCount;

        async Task IJobExecutor.ExecuteJobAsync()
        {
            if (Interlocked.Increment(ref RunningCount) <= ConcurrentCount)
            {
                await ExecuteJobAsync();
            }

            Interlocked.Decrement(ref RunningCount);
        }
    }
}