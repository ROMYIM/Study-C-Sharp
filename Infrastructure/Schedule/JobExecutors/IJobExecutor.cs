using System.Threading.Tasks;

namespace Infrastructure.Schedule.JobExecutors
{
    public interface IJobExecutor
    {
        public Task ExecuteJobAsync();
    }
}