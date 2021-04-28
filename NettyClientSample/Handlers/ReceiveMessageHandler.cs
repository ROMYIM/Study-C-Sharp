using DotNetty.Transport.Channels;
using Models;

namespace NettyClientSample.Handlers
{
    public class ReceiveMessageHandler : SimpleChannelInboundHandler<Models.Options>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, Options msg)
        {
            throw new System.NotImplementedException();
        }
    }
}