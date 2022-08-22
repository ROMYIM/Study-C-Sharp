using System.Reflection;
using DynamicProxy.Proxies;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicProxy;

internal class DispatchProxyBuilder<TService, TInstance> : IProxyBuilder where TInstance : notnull, TService
{
    private readonly IServiceProvider _serviceProvider;
    
    private readonly AspectFactory _aspectFactory;

    private readonly AspectContextFactory _aspectContextFactory;

    private readonly TInstance _originalInstance;

    public DispatchProxyBuilder(IServiceProvider serviceProvider, AspectFactory aspectFactory)
    {
        _serviceProvider = serviceProvider;
        _aspectFactory = aspectFactory;
        _aspectContextFactory = new AspectContextFactory(serviceProvider);
        _originalInstance = serviceProvider.GetRequiredService<TInstance>();
    }

    public MethodExecutor BuildMethodExecutor(MethodInfo methodInfo)
    {
        ArgumentNullException.ThrowIfNull(methodInfo);
        var aspectContext = _aspectContextFactory.Create(methodInfo, _originalInstance);
        var aspects = _aspectFactory.CreateAspect(methodInfo);
        return new MethodExecutor(aspects, aspectContext);
    }

    public TService Build()
    {
        var proxy = DispatchProxy.Create<TService, DefaultDispatchProxy<TService>>();
        var methodExecutors = new Dictionary<MethodInfo, MethodExecutor>();
        var methodInfos = typeof(TService).GetMethods(BindingFlags.Instance | BindingFlags.Public);
        foreach (var methodInfo in methodInfos)
        {
            methodExecutors.TryAdd(methodInfo, BuildMethodExecutor(methodInfo));
        }
        if (proxy is DefaultDispatchProxy<TService> dispatchProxy)
        {
            dispatchProxy.OriginalInstance = _originalInstance;
            dispatchProxy.Executors = methodExecutors;
            dispatchProxy.ServiceProvider = _serviceProvider;
        }
        return proxy;
    }

    public object? BuildProxy(Type serviceType)
    {
        if (typeof(TService) != serviceType)
        {
            throw new ArgumentException("the argument is not equal to generic type", nameof(serviceType),
                new NotSupportedException("builder can not build the proxy of serviceType"));
        }
        return Build();
    }
}
