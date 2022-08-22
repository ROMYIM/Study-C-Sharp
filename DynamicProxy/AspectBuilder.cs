using DynamicProxy.Extensions;

namespace DynamicProxy
{
    internal readonly struct AspectBuilder
    {

        private readonly List<Func<AspectDelegate, AspectDelegate>> _aspects;
            
        public IReadOnlyList<Func<AspectDelegate, AspectDelegate>> Aspects => _aspects;

        public AspectBuilder()
        {
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
            AspectDelegate next = async context =>
            {
                context.ReturnValue = context.InvokeInstanceMethod is not null ? 
                    context.InvokeInstanceMethod(context.Instance!, context.Parameters) : 
                    context.Method?.Invoke(context.Instance!, context.Parameters);
                context.InstanceMethodExecuted = true;
                await context.AwaitReturnAsync();
            };
            
            for (var i = Aspects.Count - 1; i >= 0; i--)
            {
                next = Aspects[i](next);
            }

            return next;
        }
    }
}