using System;
using System.Diagnostics.CodeAnalysis;
using DynamicProxy.Features;

namespace DynamicProxy
{
    internal class AspectContextFactory
    {
        public IServiceProvider Services { get; }
        
        internal AspectContextFactory(IServiceProvider serviceProvider)
        {
            Services = serviceProvider;
        }

        internal AspectContext Create([NotNull]MethodFeature features)
        {
            
            var context = new AspectContext(Services)
            {
                Method = features.MethodInfo,
                Instance = features.OriginalInstance,
                Parameters = features.Parameters
            };
            return context;
        }
    }
}