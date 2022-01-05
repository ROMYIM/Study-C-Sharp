using System.Threading.Tasks;
using Infrastructure.Models;

namespace Infrastructure.Schedule.Clients
{
    public interface IScheduleClient
    {
        Task CreateJobAsync(JobInfo jobInfo);
    }
}