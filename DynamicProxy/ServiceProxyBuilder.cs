using Microsoft.Extensions.DependencyInjection;

namespace DynamicProxy;

public class ServiceProxyBuilder
{
    public ServiceProxyBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; }
}