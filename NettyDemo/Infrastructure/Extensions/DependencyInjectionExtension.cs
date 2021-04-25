using System;
using DotNetty.Transport.Bootstrapping;
using Microsoft.Extensions.DependencyInjection;
using NettyDemo.Infrastructure.BackgroundServices;

namespace NettyDemo.Infrastructure.Extensions
{
    public static class DependencyInjectionExtension
    {
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
    }
}