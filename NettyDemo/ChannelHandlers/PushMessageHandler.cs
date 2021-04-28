using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using System.Net;

namespace NettyDemo.ChannelHandlers
{
    public class PushMessageHandler : ChannelHandlerAdapter
    {
        private readonly ILogger _logger;

        public PushMessageHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType());
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var clientHost = context.Channel.RemoteAddress.ToString();
            _logger.LogInformation("客户端{}发送消息{}", clientHost, message);
            base.ChannelRead(context, message);
        }

        
        public override Task WriteAsync(IChannelHandlerContext context, object message)
        {
            var ipEndPoint = context.Channel.RemoteAddress as IPEndPoint;
            var host = ipEndPoint.ToString();
            _logger.LogInformation("向客户端{}发送消息{}", host, message);
            return base.WriteAsync(context, message);
        }
    }
}