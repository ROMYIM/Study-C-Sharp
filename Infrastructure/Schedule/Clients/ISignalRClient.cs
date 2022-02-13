using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Infrastructure.Schedule.Clients
{
    public interface ISignalRClient : IAsyncDisposable
    {
        HubConnection Connection { get; }
        
        Task StartAsync(CancellationToken cancellationToken);

        Task StopAsync(CancellationToken cancellationToken);
    }
}