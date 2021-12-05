using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ILSample
{
    public class AspectBuilder
    {

        private List<Func<AspectDelegate, AspectDelegate>> _aspects;
            
        public IReadOnlyList<Func<AspectDelegate, AspectDelegate>> Aspects => _aspects;
        
        public IServiceProvider Services { get; }
        
        public AspectBuilder(IServiceProvider applicationServices)
        {
            Services = applicationServices;
            _aspects = new List<Func<AspectDelegate, AspectDelegate>>();
        }

        public AspectBuilder AddAspect(Func<AspectDelegate, AspectDelegate> aspect)
        {
            if (aspect == null) throw new ArgumentNullException(nameof(aspect));
            _aspects.Add(aspect);
            return this;
        }

        public AspectDelegate Build()
        {
            AspectDelegate next = context =>
            {
                context.ReturnValue = context.Method.Invoke(context.Instance, context.Parameters);
                return Task.CompletedTask;
            };
            
            for (int i = Aspects.Count - 1; i >= 0; i--)
            {
                next = Aspects[i](next);
            }

            return next;
        }
    }
}