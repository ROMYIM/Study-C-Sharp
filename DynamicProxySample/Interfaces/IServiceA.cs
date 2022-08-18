using DynamicProxy.Attributes;
using DynamicProxySample.Interceptors;

namespace DynamicProxySample.Interfaces;

public interface IServiceA
{
    [Aspect(typeof(LogInterceptor))]
    public int Test(int number);
}