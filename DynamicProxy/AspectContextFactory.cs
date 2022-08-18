using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using DynamicProxy.Features;

namespace DynamicProxy
{
    public class AspectContextFactory
    {
        public IServiceProvider Services { get; }
        
        public AspectContextFactory(IServiceProvider serviceProvider)
        {
            Services = serviceProvider;
        }

        internal AspectContext Create(MethodInfo methodInfo, object instance)
        {
            
            var context = new AspectContext(Services)
            {
                Method = methodInfo,
                Instance = instance
            };
            return context;
        }
    }
}