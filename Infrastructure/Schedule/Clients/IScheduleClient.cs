using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Models;
using Infrastructure.Schedule.JobExecutors;

namespace Infrastructure.Schedule.Clients
{
    public interface IScheduleClient : IDisposable
    {
        Task CreateJobAsync(JobInfo jobInfo, CancellationToken token);

        IDisposable RegisterJobExecutor<T>(JobInfo jobInfo) where T : IJobExecutor;
    }
}