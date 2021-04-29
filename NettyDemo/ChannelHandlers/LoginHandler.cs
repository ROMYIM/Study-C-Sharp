using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using NettyDemo.Infrastructure.Caches.Abbractions;
using NettyDemo.Infrastructure.Extensions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NettyDemo.ChannelHandlers
{
    public class LoginHandler : ChannelHandlerAdapter
    {
        private readonly IKeyValueCache<string, IChannel> _channelCache;

        private readonly ILogger _logger;

        public LoginHandler(IKeyValueCache<string, IChannel> channelCache, ILoggerFactory loggerFactory)
        {
            _channelCache = channelCache;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        // public override void ChannelActive(IChannelHandlerContext context)
        // {
        //     var host = context.GetRemoteHost();
        //     if (_channelCache.TryAdd(host, context.Channel))
        //         _logger.LogInformation("客户端{}已经连接到本机服务", host);
        //     else 
        //         throw new Exception($"客户端{host}建立连接失败");
        //     base.ChannelActive(context);
        // }

        // public override void ChannelInactive(IChannelHandlerContext context)
        // {
        //     var host = context.GetRemoteHost();
        //     if (_channelCache.TryRemove(host, out var channel))
        //     {
        //         if (context.Channel.Id == channel.Id)
        //             _logger.LogError("缓存连接ID{}，实际连接ID{}。两个连接的ID不一样", channel.Id, context.Channel.Id);
        //         base.ChannelInactive(context);
        //     }
        //     throw new Exception($"客户端{host}断开连接失败");
        // }
        

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            _logger.LogError(exception, "客户端与服务端连接异常", context);
            base.ExceptionCaught(context, exception);
        }
    }
}