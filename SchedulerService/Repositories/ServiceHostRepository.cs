using System.Collections.Concurrent;
using SchedulerService.Models;

namespace SchedulerService.Repositories;

public class ServiceHostRepository
{
    internal static readonly ConcurrentDictionary<string, ServiceHost?> Hosts = new();

    public bool TryGetHost(string hostName, out ServiceHost? host) => Hosts.TryGetValue(hostName, out host);

    public bool TryAddHost(ServiceHost? host) => host != null && Hosts.TryAdd(host.HostName, host);

    public void UpdateHost(ServiceHost host) => Hosts[host.HostName] = host;

    public bool TryDeleteHost(string hostName, out ServiceHost? host) => Hosts.TryRemove(hostName, out host);
}