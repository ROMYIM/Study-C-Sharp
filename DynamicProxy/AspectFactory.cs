using System;
using System.Collections.Concurrent;
using System.Reflection;
using DynamicProxy.Attributes;
using DynamicProxy.Extensions;

namespace DynamicProxy;

public class AspectFactory
{
    private readonly ConcurrentDictionary<MethodInfo, AspectDelegate> _aspects = new(Environment.ProcessorCount, 47);

    internal AspectDelegate CreateAspect(MethodInfo methodInfo)
    {
        return _aspects.GetOrAdd(methodInfo, method =>
        {
            var aspectBuilder = new AspectBuilder();
            var aspectAttribute = method.GetCustomAttribute<AspectAttribute>();
            if (aspectAttribute is null) return aspectBuilder.Build();
            foreach (var interceptorType in aspectAttribute.InterceptorTypes)
            {
                aspectBuilder.AddInterceptor(interceptorType);
            }

            return aspectBuilder.Build();
        });
    }
}