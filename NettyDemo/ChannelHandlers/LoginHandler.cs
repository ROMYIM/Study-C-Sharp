using DotNetty.Transport.Channels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NettyDemo.ChannelHandlers
{
    public class LoginHandler : ChannelHandlerAdapter
    {
        private readonly IMemoryCache _cache;

        private readonly ILogger _logger;

        public LoginHandler(IMemoryCache memoryCache, ILoggerFactory loggerFactory)
        {
            _cache = memoryCache;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        public override Task ConnectAsync(IChannelHandlerContext context, EndPoint remoteAddress, EndPoint localAddress)
        {
            var endPoint = remoteAddress as IPEndPoint;
            var host = endPoint.ToString();
            _logger.LogInformation("客户端{}已经连接到本机服务", host);
            _cache.Set<IChannelHandlerContext>(host, context);
            return base.ConnectAsync(context, remoteAddress, localAddress);
        }

        public override Task DisconnectAsync(IChannelHandlerContext context)
        {
            var endPoint = context.Channel.RemoteAddress as IPEndPoint;
            var key = endPoint.ToString();
            _logger.LogInformation("客户端{}已经断开连接", key);
            _cache.Remove(key);
            return base.DisconnectAsync(context);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            _logger.LogError(exception, "客户端建立连接失败", context);
            base.ExceptionCaught(context, exception);
        }
    }
}