using System.Net;
using DotNetty.Transport.Channels;

namespace NettyDemo.Infrastructure.Extensions
{
    public static class NettyChannelExtension
    {
        public static string GetRemoteHost(this IChannel channel)
        {
            var endPoint = channel.RemoteAddress as IPEndPoint;
            return endPoint.ToString();
        }

        public static string GetRemoteHost(this IChannelHandlerContext context)
        {
            return context.Channel.GetRemoteHost();
        }
    }
}