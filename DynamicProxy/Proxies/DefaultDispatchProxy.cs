using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicProxy.Proxies;

public class DefaultDispatchProxy<T> : DispatchProxy
{
    internal Dictionary<MethodInfo, MethodExecutor> Executors { get; set; }
    
    public T OriginalInstance { get; internal set; }
    
    public IServiceProvider ServiceProvider { get; internal set; }

    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        var methodExecutor = Executors[targetMethod];
        return methodExecutor.Execute(args);
    }
}