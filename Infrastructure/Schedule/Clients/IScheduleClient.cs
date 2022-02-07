using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Models;
using Infrastructure.Schedule.JobExecutors;

namespace Infrastructure.Schedule.Clients
{
    public interface IScheduleClient : IDisposable
    {
        Task StartAsync(CancellationToken token);

        Task StopAsync(CancellationToken token);
        
        Task CreateJobAsync(JobInfo jobInfo, CancellationToken token);

        Task ReturnResultAsync(JobExecuteResult jobResult, CancellationToken token);

    }
}