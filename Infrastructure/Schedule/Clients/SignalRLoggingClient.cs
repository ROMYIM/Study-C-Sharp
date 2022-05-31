using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Schedule.Logging;
using Infrastructure.Schedule.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace Infrastructure.Schedule.Clients
{
    internal class SignalRLoggingClient : ILoggingClient
    {
        private readonly ISignalRClient _client;

        private readonly IDisposable _callbacks;

        public SignalRLoggingClient(ScheduleLoggerProvider loggerProvider, SignalRClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient(ILoggingClient.HubName);
            
            _callbacks = Connection.On(nameof(loggerProvider.StartLoggingAsync), loggerProvider.StartLoggingAsync);
            _callbacks = Connection.On(nameof(loggerProvider.StopLoggingAsync), loggerProvider.StopLoggingAsync);
        }

        public HubConnection Connection => _client.Connection;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (Connection.State == HubConnectionState.Disconnected)
               await _client.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (Connection.State != HubConnectionState.Disconnected)
                await _client.StopAsync(cancellationToken);
        }

        public async Task PostLogsAsync(CancellationToken token = default, params LogInfo[] logs)
        {
            if (Connection.State == HubConnectionState.Connected)
                await Connection.SendAsync(nameof(PostLogsAsync), logs, token);
        }

        public ValueTask DisposeAsync()
        {
            _callbacks.Dispose();
            return _client.DisposeAsync();
        }
    }
}