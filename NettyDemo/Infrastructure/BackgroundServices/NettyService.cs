using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Libuv;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NettyDemo.Infrastructure.BackgroundServices
{
    public class NettyService : IHostedService
    {

        private ServerBootstrap _bootStrap;

        private IChannel _bootStrapChannel;

        private readonly IServiceProvider _services;

        private readonly ILogger _logger;

        public Action<IServiceProvider, ServerBootstrap> ConfigureServerBootstrap { get; set;}

        public NettyService(IServiceProvider services, ILoggerFactory loggerFactory)
        {
            _services = services;
            _logger = loggerFactory.CreateLogger(GetType());
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("运行netty服务，构建bootstrap");
            _bootStrap = new ServerBootstrap();

            _logger.LogInformation("配置bootstrap");
            ConfigureServerBootstrap(_services, _bootStrap);
            
            _logger.LogInformation("开始监听，端口8087");
            _bootStrapChannel = await _bootStrap.BindAsync(IPAddress.Parse("127.0.0.1"), 8087);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("netty服务停止运行");
            return _bootStrapChannel.CloseAsync();
        }
    }
}