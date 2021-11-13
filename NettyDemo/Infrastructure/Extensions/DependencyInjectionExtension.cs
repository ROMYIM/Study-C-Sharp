using System;
using DotNetty.Transport.Bootstrapping;
using Microsoft.Extensions.DependencyInjection;
using NettyDemo.Infrastructure.BackgroundServices;
using Zaabee.RabbitMQ.Serializer.Abstraction;
using Zaabee.RabbitMQ.SystemTextJson;
using Zaabee.RabbitMQ;
using Zaabee.RabbitMQ.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace NettyDemo.Infrastructure.Extensions
{
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// 注册netty后台服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configureServerBootstrap">配置<c>ServerBootstrap</c>的委托</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddNettyService(this IServiceCollection services, Action<IServiceProvider, ServerBootstrap> configureServerBootstrap)
        {
            if (configureServerBootstrap == null)
                throw new ArgumentNullException(nameof(configureServerBootstrap));
            services.TryAddSingleton<NettyService>();
            services.AddHostedService<NettyService>(sp => 
            {
                var service = sp.GetRequiredService<NettyService>();
                service.ConfigureServerBootstrap = configureServerBootstrap;
                return service;
            });

            return services;
        }

        /// <summary>
        /// 向容器注册mqclient服务。
        /// 通过读取配置文件构建mq相关的配置选项
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="optionsName">配置文件中mq配置选项对应的键名</param>
        /// <param name="configuration">配置集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddMqClient(this IServiceCollection services, string optionsName, IConfiguration configuration)
        {
            services.TryAddSingleton<ISerializer, Serializer>();
            services.Configure<MqConfig>(configuration.GetSection(optionsName));
            services.TryAddSingleton<IZaabeeRabbitMqClient>(serviceProvider => 
            {
                var mqConfigOptions = serviceProvider.GetRequiredService<IOptions<MqConfig>>();
                var serializer = serviceProvider.GetRequiredService<ISerializer>();
                return new ZaabeeRabbitMqClient(mqConfigOptions.Value, serializer);
            });

            return services;
        }

        /// <summary>
        /// 向容器注册mqclient服务。
        /// 通过委托构建mq相关的配置选项
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="optionsBuilder">构建mq配置选项的委托</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddMqClient(this IServiceCollection services, Action<MqConfig> optionsBuilder)
        {
            if (optionsBuilder == null)
                throw new ArgumentNullException(nameof(optionsBuilder));

            services.TryAddSingleton<ISerializer, Serializer>();
            services.TryAddSingleton<IZaabeeRabbitMqClient>(serviceProvider => 
            {
                var serializer = serviceProvider.GetRequiredService<ISerializer>();
                var options = new MqConfig();
                optionsBuilder.Invoke(options);
                return new ZaabeeRabbitMqClient(options, serializer);
            });

            return services;
        }

        /// <summary>
        /// 添加消息队列消费者后台服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="subscribeConsumers">订阅消费者的委托</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddMqConsumerService(this IServiceCollection services, 
            Action<IServiceProvider, IZaabeeRabbitMqClient> subscribeConsumers)
        {
            if (subscribeConsumers == null) 
                throw new ArgumentNullException(nameof(subscribeConsumers));

            services.TryAddSingleton<MessageQueueService>();
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