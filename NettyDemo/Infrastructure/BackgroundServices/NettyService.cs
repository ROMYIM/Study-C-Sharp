using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Libuv;
using Microsoft.Extensions.Hosting;

namespace NettyDemo.Infrastructure.BackgroundServices
{
    public class NettyService : IHostedService
    {

        private ServerBootstrap _bootStrap;

        private IChannel _bootStrapChannel;

        private readonly IServiceProvider _services;

        public Action<IServiceProvider, ServerBootstrap> ConfigureServerBootstrap { get; set;}

        public NettyService(IServiceProvider services)
        {
            _services = services;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _bootStrap = new ServerBootstrap();

            ConfigureServerBootstrap(_services, _bootStrap);
            
            _bootStrapChannel = await _bootStrap.BindAsync(IPAddress.Loopback, 8087);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _bootStrapChannel.CloseAsync();
        }
    }
}