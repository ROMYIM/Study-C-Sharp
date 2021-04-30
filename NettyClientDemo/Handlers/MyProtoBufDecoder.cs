using System;
using DotNetty.Codecs.Protobuf;
using DotNetty.Transport.Channels;
using Google.Protobuf;

namespace NettyClientDemo.Handlers
{
    public class MyProtoBufDecoder : ProtobufDecoder
    {
        public MyProtoBufDecoder(MessageParser messageParser) : base(messageParser)
        {
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {

            System.Console.WriteLine(exception.Message);
            System.Console.WriteLine(exception.StackTrace);
            System.Console.WriteLine(exception.Source);
            System.Console.WriteLine(exception.InnerException.Message);
            System.Console.WriteLine(exception.InnerException.StackTrace);
            System.Console.WriteLine(exception.InnerException.Source);
            base.ExceptionCaught(context, exception);
        }
    }
}