using System;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicProxy.Options
{
    public class ProxyOptions
    {
        public const ServiceLifetime DefaultLifetime = ServiceLifetime.Transient;
        
        public Type ServiceType { get; set; }

        public Type InstanceType { get; set; }

        public Func<IServiceProvider, object> InstanceFactory { get; set; }

        public ServiceLifetime Lifetime { get; set; } = DefaultLifetime;

        public void Deconstruct(out Type serviceType, out Type instanceType,
            out Func<IServiceProvider, object> instanceFactory, out ServiceLifetime lifetime)
        {
            serviceType = ServiceType;
            instanceType = InstanceType;
            instanceFactory = InstanceFactory;
            lifetime = Lifetime;
        }
    }
}