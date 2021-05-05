using System;
using DotNetty.Transport.Bootstrapping;
using Microsoft.Extensions.DependencyInjection;
using NettyDemo.Infrastructure.BackgroundServices;
using Zaabee.RabbitMQ.Serializer.Abstraction;
using Zaabee.RabbitMQ.SystemTextJson;
using Zaabee.RabbitMQ;
using Zaabee.RabbitMQ.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace NettyDemo.Infrastructure.Extensions
{
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// 注册netty后台服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configureServerBootstrap"></param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddNettyService(this IServiceCollection services, Action<IServiceProvider, ServerBootstrap> configureServerBootstrap)
        {
            if (configureServerBootstrap == null)
                throw new ArgumentNullException(nameof(configureServerBootstrap));

            services.AddHostedService<NettyService>(services => 
            {
                var service = new NettyService(services);
                service.ConfigureServerBootstrap = configureServerBootstrap;
                return service;
            });

            return services;
        }

        public static IServiceCollection AddMqClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ISerializer, Serializer>();
            services.Configure<MqConfig>(configuration.GetSection("RabbitMq"));
            services.AddSingleton<IZaabeeRabbitMqClient, ZaabeeRabbitMqClient>(serviceProvider => 
            {
                var mqConfigOptions = serviceProvider.GetRequiredService<IOptions<MqConfig>>();
                var serializer = serviceProvider.GetRequiredService<ISerializer>();
                return new ZaabeeRabbitMqClient(mqConfigOptions.Value, serializer);
            });

            return services;
        }

        public static IServiceCollection AddMqConsumerService(this IServiceCollection services, Action<IZaabeeRabbitMqClient> subscribeConsumers)
        {
            if (subscribeConsumers == null) 
                throw new ArgumentNullException(nameof(subscribeConsumers));

            services.AddSingleton<MessageQueueService>();
            services.AddHostedService<MessageQueueService>(serviceProvider => 
            {
                var service = serviceProvider.GetRequiredService<MessageQueueService>();
                service.SubscribeConsumers = subscribeConsumers;
                return service;
            });

            return services;
        }
    }
}