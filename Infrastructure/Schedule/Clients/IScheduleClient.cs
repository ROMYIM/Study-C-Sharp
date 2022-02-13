using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Models;
using Infrastructure.Schedule.Models;

namespace Infrastructure.Schedule.Clients
{
    public interface IScheduleClient : ISignalRClient
    {
        const string HubName = "Schedule";
        
        Task CreateJobAsync(JobInfo jobInfo, CancellationToken token);

        Task ReturnResultAsync(JobExecuteResult jobResult, CancellationToken token);

    }
}