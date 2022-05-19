using Infrastructure.Schedule.Models;

namespace SchedulerService.Models;

public class ServiceHost
{
    public string HostName { get; set; } = null!;

    public string ExecutePath { get; set; } = null!;

    public HostStatus HostStatus { get; set; }

    public IEnumerable<JobInfo>? JobInfos { get; set; }
}