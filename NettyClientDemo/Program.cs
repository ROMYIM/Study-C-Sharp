using System;
using System.Net;
using System.Threading.Tasks;
using DotNetty.Codecs.Protobuf;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using NettyClientDemo.Handlers;

namespace NettyClientDemo
{
    class Program
    {
        private static NettyClient _client = new NettyClient(bootstrap =>
            {
                var group = new MultithreadEventLoopGroup();

                bootstrap
                .Group(group)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Option(ChannelOption.AutoRead, true)
                .Option(ChannelOption.ConnectTimeout, TimeSpan.FromMilliseconds(500))
                .Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    var pipeline = channel.Pipeline;

                    pipeline.AddLast(new ProtobufVarint32FrameDecoder());
                    pipeline.AddLast(new MyProtoBufDecoder(Models.Options.Parser));
                    // pipeline.AddLast(new ProtobufVarint32FrameDecoder());
                    pipeline.AddLast(new ProtobufVarint32LengthFieldPrepender());
                    pipeline.AddLast(new ProtobufEncoder());

                    pipeline.AddLast(new ReceiveMessageHandler());
                }));
            });

        async static Task Main(string[] args)
        {
            System.Console.WriteLine("客户端启动");
            var channel = await  _client.ConnectAsync(IPEndPoint.Parse("127.0.0.1:8087"));

            await Task.Delay(TimeSpan.FromMinutes(5));

            await channel.CloseAsync();
        }
    }
}
