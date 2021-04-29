using System;
using System.Net;
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;

namespace NettyClientDemo
{
    public class NettyClient
    {
        private Bootstrap _bootstrap;

        private IChannel _channel;

        public NettyClient(Action<Bootstrap> configureBootstrap)
        {
            if (configureBootstrap == null) throw new ArgumentNullException(nameof(configureBootstrap));
            _bootstrap = new Bootstrap();
            configureBootstrap(_bootstrap);
        }

        public async Task<IChannel> ConnectAsync(IPEndPoint endpoint)
        {
            _channel = await _bootstrap.ConnectAsync(endpoint);
            return _channel;
        }

        public Task SendMessagAsync(Models.Options options)
        {
            return _channel.WriteAndFlushAsync(options);
        }
    }
}