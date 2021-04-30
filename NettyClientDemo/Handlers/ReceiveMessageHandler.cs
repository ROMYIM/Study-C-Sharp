using DotNetty.Transport.Channels;
using Models;
using System;
using System.Net;

namespace NettyClientDemo.Handlers
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

        public override void ChannelRead(IChannelHandlerContext ctx, object msg)
        {
            System.Console.WriteLine(msg);
            base.ChannelRead(ctx, msg);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            System.Console.WriteLine(exception.Message);
            System.Console.WriteLine(exception.StackTrace);
            base.ExceptionCaught(context, exception);
        }
    }
}