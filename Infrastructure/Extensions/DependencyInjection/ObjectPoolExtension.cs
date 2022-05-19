using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;

namespace Infrastructure.Extensions.DependencyInjection;

public static class ObjectPoolExtension
{
    /// <summary>
    /// DI添加<c>ObjectPool&lt;StringBuilder&gt;</c>
    /// <seealso cref="ObjectPoolProvider"/>
    /// <seealso cref="StringBuilderPooledObjectPolicy"/>
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <returns></returns>
    public static IServiceCollection AddStringBuilderPool(this IServiceCollection services)
    {
        services.TryAddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

        services.TryAddSingleton(serviceProvider =>
        {
            var provider = serviceProvider.GetRequiredService<ObjectPoolProvider>();
            var policy = new StringBuilderPooledObjectPolicy();
            return provider.Create(policy);
        });

        return services;
    }
}