using System;
using Microsoft.Extensions.DependencyInjection;

namespace SecurityTest
{
    public static class ServiceProviderExtension
    {
        private readonly static IServiceCollection _services = new ServiceCollection();

        public static IServiceProvider ServiceProvider { get; }

        static ServiceProviderExtension()
        {
            
            ServiceProvider = _services.BuildServiceProvider();
        }
    }
}