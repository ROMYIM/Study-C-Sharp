using Xunit;
using DotNetty.Codecs.Protobuf;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using NettyClientSample.Handlers;
using System.Net;
using System.Threading.Tasks;
using System;

namespace NettyClientSample
{
    public class ProtobufTest
    {
        private NettyClient _client;

        [Fact]
        public async Task ConnectionTest()
        {
        //Given
            _client = new NettyClient(bootstrap =>
            {
                var group = new MultithreadEventLoopGroup();

                bootstrap
                .Group(group)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Option(ChannelOption.ConnectTimeout, TimeSpan.FromMilliseconds(500))
                .Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    var pipeline = channel.Pipeline;

                    pipeline.AddLast(new ProtobufVarint32FrameDecoder());
                    pipeline.AddLast(new ProtobufDecoder(Models.Options.Parser));
                    pipeline.AddLast(new ProtobufVarint32LengthFieldPrepender());
                    pipeline.AddLast(new ProtobufEncoder());
                    pipeline.AddLast(new ReceiveMessageHandler());
                }));
            });
        //When
            var channel = await _client.ConnectAsync(IPEndPoint.Parse("127.0.0.1:8087"));
            for (int i = 0; i < 3; i++)
            {
                await channel.WriteAndFlushAsync(new Models.Options
                {
                    Id = "1",
                    Type = "PostTyp"
                });

                await Task.Delay(TimeSpan.FromMinutes(1));

                await channel.WriteAndFlushAsync(new Models.Options
                {
                    Id = "2",
                    Type = "PostTyp"
                });
            }
        //Then
            await channel.CloseAsync();
        }
    }
}