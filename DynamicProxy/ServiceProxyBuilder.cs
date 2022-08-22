using DynamicProxy.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DynamicProxy;

public class ServiceProxyBuilder : IProxyBuilder
{
    private IServiceProvider? _provider;

    public ServiceProxyBuilder(IServiceCollection services)
    {
        Services = services;
        Options = new Dictionary<Type, ProxyOptions>();
        services.TryAddSingleton<AspectFactory>();
    }

    public IServiceCollection Services { get; }
    
    internal Dictionary<Type, ProxyOptions> Options { get; }

    internal void AddOptions(ProxyOptions proxyOptions) => Options[proxyOptions.ServiceType] = proxyOptions;

    internal void Build(Type serviceType)
    {
        ArgumentNullException.ThrowIfNull(serviceType);
        
        if (!Options.TryGetValue(serviceType, out var proxyOptions)) return;
        var (proxyServiceType, instanceType, instanceFactory, lifetime) = proxyOptions;
            
        var dispatchProxyBuildType = GenerateProxyBuilderType(proxyServiceType, instanceType);
        Services.AddScoped(dispatchProxyBuildType);

        Services.TryAdd(instanceFactory is not null
            ? ServiceDescriptor.Describe(instanceType, instanceFactory, lifetime)
            : ServiceDescriptor.Describe(instanceType, instanceType, lifetime));

        Services.Add(ServiceDescriptor.Describe(serviceType, provider =>
        {
            var proxyBuilder = provider.GetRequiredService(dispatchProxyBuildType) as IProxyBuilder;
            return proxyBuilder?.BuildProxy(serviceType) ?? throw new NotSupportedException($"Proxy builder can not build {serviceType.FullName} proxy");
        }, lifetime));
    }
    
    internal static Type GenerateProxyBuilderType(Type serviceType, Type instanceType)
    {
        if (!serviceType.IsInterface)
            throw new ArgumentException("serviceType must be a interface", nameof(serviceType), new NotSupportedException());
        
        if (!instanceType.IsClass)
            throw new ArgumentException("instanceType must be a class", nameof(instanceType), new NotSupportedException());

        if (!serviceType.IsAssignableFrom(instanceType))
            throw new NotSupportedException("instanceType must implement serviceType");

        return typeof(DispatchProxyBuilder<,>).MakeGenericType(serviceType, instanceType);
    }

    public object? BuildProxy(Type serviceType)
    {
        _provider ??= Services.BuildServiceProvider();

        return _provider.GetService(serviceType);
    }
}