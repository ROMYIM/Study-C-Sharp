using DynamicProxy.Attributes;
using DynamicProxySample.Interceptors;

namespace DynamicProxySample.Interfaces;

public interface IServiceA
{
    [Aspect<LogInterceptor>]
    public int Test(ref int number);
    
    [Aspect<LogInterceptor, TransactionalInterceptor>]
    public Task TestDbAsync(int id, out string name);
}