using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace NettyDemo.ChannelHandlers
{
    public class PushMessageHandler : ChannelHandlerAdapter
    {
        private readonly ILogger _logger;

        private readonly IMemoryCache _cache;

        private static readonly ConcurrentDictionary<string, IChannel> _channelContextMap = new ConcurrentDictionary<string, IChannel>();

        public PushMessageHandler(ILoggerFactory loggerFactory, IMemoryCache cache)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _cache = cache;
        }

        public override Task ConnectAsync(IChannelHandlerContext context, EndPoint remoteAddress, EndPoint localAddress)
        {
            var endPoint = remoteAddress as IPEndPoint;
            var host = endPoint.ToString();
            _logger.LogInformation("客户端{}已经连接到本机服务", host);
            _cache.Set<IChannelHandlerContext>(host, context);
            return base.ConnectAsync(context, remoteAddress, localAddress);
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var clientHost = context.Channel.RemoteAddress.ToString();
            _logger.LogInformation("客户端{}发送消息{}", clientHost, message);
            _logger.LogInformation("消息类型{}", message.GetType().FullName);
            
            if (_channelContextMap.TryGetValue(clientHost, out var channel)
                && channel.Active)
                _logger.LogInformation("客户端{}存在注册表中", clientHost);
            else
            {
                _logger.LogError("客户端{}不在注册表中", clientHost);
                _channelContextMap.TryAdd(clientHost, context.Channel);
            }
            base.ChannelRead(context, message);
        }

        
        public override Task WriteAsync(IChannelHandlerContext context, object message)
        {
            var ipEndPoint = context.Channel.RemoteAddress as IPEndPoint;
            var host = ipEndPoint.ToString();
            _logger.LogInformation("向客户端{}发送消息{}", host, message);
            return base.WriteAsync(context, message);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, System.Exception exception)
        {
            _logger.LogError(exception, "客户端通信失败", context);
            base.ExceptionCaught(context, exception);
        }
    }
}