using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Schedule.Options;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.Clients
{
    internal class SignalRClient : ISignalRClient
    {
        private readonly HubConnection _connection;

        private bool _disposed;

        public SignalRClient(IOptions<SignalRClientOptions> options, string hubName)
        {
            var clientOptions = options.Value;
            _connection = new HubConnectionBuilder()
                .WithUrl($"{clientOptions.Host}/{hubName}")
                .WithAutomaticReconnect()
                .Build();
            // _connection.HandshakeTimeout = clientOptions.HandShakeTimeout;
            // _connection.KeepAliveInterval = clientOptions.KeepAliveInterval;
            // _connection.ServerTimeout = clientOptions.ServerTimeout;
        }

        HubConnection ISignalRClient.Connection => _connection;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _connection.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _connection.StopAsync(cancellationToken);
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                await _connection.DisposeAsync();
                _disposed = true;
            }
        }
    }
}