using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Schedule.Models;

namespace Infrastructure.Schedule.Clients
{
    public interface ILoggingClient : ISignalRClient
    {
        const string HubName = "Logging";
        
        Task PostLogsAsync(CancellationToken token = default, params LogInfo[] logs);
    }
}