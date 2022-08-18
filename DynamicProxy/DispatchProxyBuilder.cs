using System;
using System.Collections.Generic;
using System.Reflection;
using DynamicProxy.Attributes;
using DynamicProxy.Extensions;
using DynamicProxy.Proxies;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicProxy;

public class DispatchProxyBuilder<TInterface, TInstance> where TInstance : TInterface
{
    private readonly IServiceProvider _serviceProvider;
    
    private readonly AspectBuilder _aspectBuilder;

    private readonly AspectContextFactory _aspectContextFactory;

    private readonly TInstance _originalInstance;

    public DispatchProxyBuilder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _aspectBuilder = new AspectBuilder(serviceProvider);
        _aspectContextFactory = new AspectContextFactory(serviceProvider);
        _originalInstance = serviceProvider.GetService<TInstance>();
    }

    public MethodExecutor BuildMethodExecutor(MethodInfo methodInfo)
    {
        ArgumentNullException.ThrowIfNull(methodInfo);
        var aspectContext = _aspectContextFactory.Create(methodInfo, _originalInstance);
        var aspectAttribute = methodInfo.GetCustomAttribute<AspectAttribute>();
        if (aspectAttribute is not null)
        {
            foreach (var interceptorType in aspectAttribute.InterceptorTypes)
            {
                _aspectBuilder.AddInterceptor(interceptorType);
            }
        }

        var aspects = _aspectBuilder.Build();
        return new MethodExecutor(aspects, aspectContext);
    }

    public TInterface Build()
    {
        var proxy = DispatchProxy.Create<TInterface, DefaultDispatchProxy<TInterface>>();
        var methodExecutors = new Dictionary<MethodInfo, MethodExecutor>();
        var methodInfos = typeof(TInterface).GetMethods(BindingFlags.Instance | BindingFlags.Public);
        foreach (var methodInfo in methodInfos)
        {
            methodExecutors.TryAdd(methodInfo, BuildMethodExecutor(methodInfo));
        }
        if (proxy is DefaultDispatchProxy<TInterface> dispatchProxy)
        {
            dispatchProxy.OriginalInstance = _originalInstance;
            dispatchProxy.Executors = methodExecutors;
            dispatchProxy.ServiceProvider = _serviceProvider;
        }
        return proxy;
    }
}