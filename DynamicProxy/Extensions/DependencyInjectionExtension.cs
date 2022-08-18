using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DynamicProxy.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddServiceProxy<TInterface, TService>(this IServiceCollection services, ServiceLifetime lifetime) where TService : TInterface
    {
        services.TryAdd(ServiceDescriptor.Describe(typeof(TService), typeof(TService), lifetime));
        return services;
    }
}