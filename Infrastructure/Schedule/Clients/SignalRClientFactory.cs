using System;
using System.Collections.Concurrent;
using Infrastructure.Schedule.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.Clients
{
    internal class SignalRClientFactory
    {
        private  readonly IOptions<SignalRClientOptions> _clientOptions;

        private readonly ConcurrentDictionary<string, Lazy<ISignalRClient>> _clients;

        public SignalRClientFactory(IOptions<SignalRClientOptions> clientOptions)
        {
            _clientOptions = clientOptions;
            _clients = new ConcurrentDictionary<string, Lazy<ISignalRClient>>();
        }

        internal ISignalRClient CreateClient(string routePattern)
        {
            if (string.IsNullOrWhiteSpace(routePattern))
                throw new ArgumentNullException(nameof(routePattern));
            return _clients.GetOrAdd(routePattern,
                s => new Lazy<ISignalRClient>(() => new SignalRClient(_clientOptions, s))).Value;
        }
    }
}