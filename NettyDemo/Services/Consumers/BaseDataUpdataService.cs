using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using NettyDemo.Infrastructure.Caches.Abbractions;

namespace NettyDemo.Services.Consumers
{
    public class BaseDataUpdataService
    {
        private readonly IKeyValueCache<string, IChannel> _channelCache;

        private readonly ILogger _logger;

        public BaseDataUpdataService(ILoggerFactory loggerFactory, IKeyValueCache<string, IChannel> channelCache)
        {
            
        }
    }
}