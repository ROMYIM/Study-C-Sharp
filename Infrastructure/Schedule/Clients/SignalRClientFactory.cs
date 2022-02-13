using System;
using Infrastructure.Schedule.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.Clients
{
    internal class SignalRClientFactory
    {
        private  readonly IOptions<SignalRClientOptions> _clientOptions;

        public SignalRClientFactory(IOptions<SignalRClientOptions> clientOptions)
        {
            _clientOptions = clientOptions;
        }

        internal ISignalRClient CreateClient(string hubName)
        {
            if (string.IsNullOrWhiteSpace(hubName))
                throw new ArgumentNullException(nameof(hubName));
            return new SignalRClient(_clientOptions, hubName);
        }
    }
}