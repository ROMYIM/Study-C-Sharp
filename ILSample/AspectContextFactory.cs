using System;

namespace ILSample
{
    internal class AspectContextFactory
    {
        public IServiceProvider Services { get; }
        
        internal AspectContextFactory(IServiceProvider serviceProvider)
        {
            Services = serviceProvider;
        }

        internal AspectContext Create() => new AspectContext(Services);
    }
}