using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Zaabee.RabbitMQ.Abstractions;

namespace NettyDemo.Infrastructure.BackgroundServices
{
    public class MessageQueueService : IHostedService
    {
        private readonly IZaabeeRabbitMqClient _mqClient;

        private readonly IServiceProvider _services;

        private readonly ILogger _logger;

        public Action<IServiceProvider, IZaabeeRabbitMqClient> SubscribeConsumers { get; set; }

        public MessageQueueService(IZaabeeRabbitMqClient mqClient, ILoggerFactory loggerFactory, IServiceProvider service)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _mqClient = mqClient;
            _services = service;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (SubscribeConsumers == null) throw new InvalidOperationException("没有订阅任何消费者");
            _logger.LogInformation("服务启动，订阅消费者");
            SubscribeConsumers(_services, _mqClient);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("服务停止");
            _mqClient.Dispose();
            return Task.CompletedTask;
        }
    }
}