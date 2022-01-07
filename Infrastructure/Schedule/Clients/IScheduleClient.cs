using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Models;

namespace Infrastructure.Schedule.Clients
{
    public interface IScheduleClient : IDisposable
    {
        Task CreateJobAsync(JobInfo jobInfo, CancellationToken token);
    }
}