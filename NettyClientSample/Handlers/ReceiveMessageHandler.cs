using DotNetty.Transport.Channels;
using Models;
using System.Net;

namespace NettyClientSample.Handlers
{
    public class ReceiveMessageHandler : SimpleChannelInboundHandler<Models.Options>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, Options msg)
        {
            var endPoint = ctx.Channel.RemoteAddress as IPEndPoint;
            System.Console.WriteLine("服务主机{0}", endPoint);
            System.Console.WriteLine("接收消息{0}", msg);

            ctx.Channel.WriteAndFlushAsync(msg);
        }
    }
}