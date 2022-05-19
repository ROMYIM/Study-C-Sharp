using SchedulerService.Models;
using SchedulerService.Repositories;

namespace SchedulerService.Query;

public class ServiceHostQueryService
{
    public IEnumerable<ServiceHost?> GetAllServiceHost()
    {
        return ServiceHostRepository.Hosts.Values;
    }
}