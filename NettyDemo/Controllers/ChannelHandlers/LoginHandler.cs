using DotNetty.Transport.Channels;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NettyDemo.Controllers.ChannelHandlers
{
    public class LoginHandler : ChannelHandlerAdapter
    {
        private readonly IMemoryCache _cache;

        public LoginHandler(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public override Task ConnectAsync(IChannelHandlerContext context, EndPoint remoteAddress, EndPoint localAddress)
        {
            var endPoint = remoteAddress as IPEndPoint;
            var host = endPoint.ToString();
            _cache.Set<IChannelHandlerContext>(host, context);
            return base.ConnectAsync(context, remoteAddress, localAddress);
        }

        public override Task DisconnectAsync(IChannelHandlerContext context)
        {
            var endPoint = context.Channel.RemoteAddress as IPEndPoint;
            var key = endPoint.ToString();
            _cache.Remove(key);
            return base.DisconnectAsync(context);
        }
    }
}