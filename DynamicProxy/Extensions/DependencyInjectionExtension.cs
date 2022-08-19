using System;
using System.Reflection;
using DynamicProxy.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DynamicProxy.Extensions;

public static class DependencyInjectionExtension
{

    public static ServiceProxyBuilder AddServiceProxy<TInterface, TService>(this IServiceCollection services, Action<ServiceProxyBuilder> configure = null, ServiceLifetime lifetime = ServiceLifetime.Transient) where TService : TInterface
    {
        var builder = new ServiceProxyBuilder(services);
        services.TryAddSingleton<AspectFactory>();
        services.TryAdd(ServiceDescriptor.Describe(typeof(TService), typeof(TService), lifetime));
        if (configure is null)
        {
            builder.AddInterceptors<TInterface>(lifetime);
        }
        else
        {
            configure(builder);
        }
        services.Add(ServiceDescriptor.Describe(typeof(DispatchProxyBuilder<TInterface, TService>),
            typeof(DispatchProxyBuilder<TInterface, TService>), ServiceLifetime.Scoped));
        services.Add(ServiceDescriptor.Describe(typeof(TInterface), provider =>
        {
            var proxyBuilder = provider.GetRequiredService<DispatchProxyBuilder<TInterface, TService>>();
            return proxyBuilder.Build();
        }, lifetime));
        return builder;
    }

    public static ServiceProxyBuilder AddInterceptor<TInterceptor>(this ServiceProxyBuilder builder, ServiceLifetime interceptorLifeTime)
        where TInterceptor : IInterceptor
    {
        return builder.AddInterceptor(typeof(TInterceptor), interceptorLifeTime);
    }
    
    public static ServiceProxyBuilder AddInterceptor(this ServiceProxyBuilder builder, Type interceptorType, ServiceLifetime interceptorLifeTime)
    {
        var services = builder.Services;
        services.Add(ServiceDescriptor.Describe(interceptorType, interceptorType, interceptorLifeTime));
        return builder;
    }

    public static ServiceProxyBuilder AddInterceptors<TInterface>(this ServiceProxyBuilder builder,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        var methods = typeof(TInterface).GetMethods(BindingFlags.Instance | BindingFlags.Public);
        foreach (var method in methods)
        {
            var aspect = method.GetCustomAttribute<AspectAttribute>();
            if (aspect is null) continue;
            foreach (var interceptorType in aspect.InterceptorTypes)
            {
                builder.AddInterceptor(interceptorType, lifetime);
            }
        }
        return builder;
    }
}