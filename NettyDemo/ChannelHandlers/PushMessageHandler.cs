using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Collections.Concurrent;
using NettyDemo.Infrastructure.Caches.Abbractions;
using NettyDemo.Infrastructure.Extensions;
using System;

namespace NettyDemo.ChannelHandlers
{
    public class PushMessageHandler : ChannelHandlerAdapter
    {
        private readonly ILogger _logger;

        private readonly IKeyValueCache<string, IChannel> _channelCache;

        public PushMessageHandler(ILoggerFactory loggerFactory, IKeyValueCache<string, IChannel> channelCache)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _channelCache = channelCache;
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            var host = context.GetRemoteHost();
            if (_channelCache.TryAdd(host, context.Channel))
                _logger.LogInformation("客户端{}已经连接到本机服务", host);
            else 
                throw new Exception($"客户端{host}建立连接失败");
            base.ChannelActive(context);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            var host = context.GetRemoteHost();
            if (_channelCache.TryRemove(host, out var channel))
            {
                if (context.Channel.Id != channel.Id)
                    _logger.LogError("缓存连接ID{}，实际连接ID{}。两个连接的ID不一样", channel.Id, context.Channel.Id);
                base.ChannelInactive(context);
                return;
            }
            throw new Exception($"客户端{host}断开连接失败");            
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var clientHost = context.GetRemoteHost();
            _logger.LogInformation("客户端{}发送消息{}", clientHost, message);
            _logger.LogInformation("消息类型{}", message.GetType().FullName);
            
            if (_channelCache.TryGetValue(clientHost, out var channel)
                && channel.Active)
                _logger.LogInformation("客户端{}存在注册表中", clientHost);
            else
            {
                _logger.LogError("客户端{}不在注册表中", clientHost);
                // _ch.TryAdd(clientHost, context.Channel);
            }
            base.ChannelRead(context, message);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, System.Exception exception)
        {
            _logger.LogError(exception, "客户端通信失败", context);
            base.ExceptionCaught(context, exception);
        }
    }
}