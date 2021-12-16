using System;
using System.Diagnostics.CodeAnalysis;

namespace ILSample
{
    internal class AspectContextFactory
    {
        public IServiceProvider Services { get; }
        
        internal AspectContextFactory(IServiceProvider serviceProvider)
        {
            Services = serviceProvider;
        }

        [return: NotNull]
        internal AspectContext Create(MethodFeature methodFeature)
        {
            var context = new AspectContext(Services)
            {
                Instance = methodFeature.OriginalInstance,
                Method = methodFeature.MethodInfo,
                Parameters = methodFeature.Parameters
            };
            return context;
        }
    }
}