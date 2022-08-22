using System.Reflection;

namespace DynamicProxy.Proxies;

public class DefaultDispatchProxy<T> : DispatchProxy
{
    public DefaultDispatchProxy()
    {
        Executors = new Dictionary<MethodInfo, MethodExecutor>();
    }

    internal Dictionary<MethodInfo, MethodExecutor> Executors { get; set; }
    
    public T? OriginalInstance { get; internal set; }
    
    public IServiceProvider? ServiceProvider { get; internal set; }

    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        ArgumentNullException.ThrowIfNull(targetMethod);
        ArgumentNullException.ThrowIfNull(args);
        var methodExecutor = Executors[targetMethod];
        return methodExecutor.Execute(args);
    }
}