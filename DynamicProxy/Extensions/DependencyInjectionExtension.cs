using System.Reflection;
using DynamicProxy.Attributes;
using DynamicProxy.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DynamicProxy.Extensions;

public static class DependencyInjectionExtension
{
    public static ServiceProxyBuilder AddServiceProxy(this IServiceCollection services)
    {
        return new ServiceProxyBuilder(services);
    }

    public static ServiceProxyBuilder ConfigurePoxy(this ServiceProxyBuilder serviceProxyBuilder, Action<ProxyOptions> configure)
    {
        var proxyOptions = new ProxyOptions();
        configure(proxyOptions);
        serviceProxyBuilder.AddOptions(proxyOptions);
        serviceProxyBuilder.Build(proxyOptions.ServiceType);
        return serviceProxyBuilder;
    }

    public static ServiceProxyBuilder ConfigureServiceProxy(this ServiceProxyBuilder serviceProxyBuilder,
        Type serviceType, Type instanceType, Action<ServiceProxyBuilder>? configure = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        var builder = serviceProxyBuilder.ConfigurePoxy(options =>
        {
            options.ServiceType = serviceType;
            options.InstanceType = instanceType;
            options.Lifetime = lifetime;
        });

        configure?.Invoke(builder);

        return builder.AddInterceptors(serviceType, lifetime);
    }

    public static ServiceProxyBuilder ConfigureServiceProxy<TService, TInstance>(
        this ServiceProxyBuilder serviceProxyBuilder, Action<ServiceProxyBuilder>? configure = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient) where TInstance : TService
    {
        return serviceProxyBuilder.ConfigureServiceProxy(typeof(TService), typeof(TInstance), configure, lifetime);
    }

    public static ServiceProxyBuilder AddInterceptor<TInterceptor>(this ServiceProxyBuilder builder, ServiceLifetime interceptorLifeTime)
        where TInterceptor : IInterceptor
    {
        return builder.AddInterceptor(typeof(TInterceptor), interceptorLifeTime);
    }
    
    public static ServiceProxyBuilder AddInterceptor(this ServiceProxyBuilder builder, Type interceptorType, ServiceLifetime interceptorLifeTime)
    {
        var services = builder.Services;
        services.TryAdd(ServiceDescriptor.Describe(interceptorType, interceptorType, interceptorLifeTime));
        return builder;
    }

    public static ServiceProxyBuilder AddInterceptors(this ServiceProxyBuilder builder, Type serviceType,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        var methods = serviceType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
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