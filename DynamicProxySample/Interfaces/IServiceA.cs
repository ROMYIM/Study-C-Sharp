using DynamicProxy.Attributes;
using DynamicProxySample.Interceptors;

namespace DynamicProxySample.Interfaces;

public interface IServiceA
{
    [Aspect(typeof(LogInterceptor))]
    public int Test( int number);
    
    [Aspect(typeof(LogInterceptor), typeof(TransactionalInterceptor))]
    public Task<string> TestDbAsync(int id, string name);
}