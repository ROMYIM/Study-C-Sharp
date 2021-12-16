using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using DynamicProxy.Extensions;

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
            AspectDelegate next = async context =>
            {
                var returnValue = context.Method.Invoke(context.Instance, context.Parameters);

                var returnType = context.Method.ReturnType;
                if (returnType == typeof(void))
                {
                    context.ReturnValue = null;
                    return;
                }

                await AwaitReturnAsync(returnValue);
                context.ReturnValue = returnValue;
            };
            
            for (int i = Aspects.Count - 1; i >= 0; i--)
            {
                next = Aspects[i](next);
            }

            return next;
        }
        
        private static async ValueTask AwaitReturnAsync(object? returnValue)
        {
            switch (returnValue)
            {
                case null:
                    break;
                case Task task:
                    await task;
                    break;
                case ValueTask valueTask:
                    await valueTask;
                    break;
                default:
                    var returnValueTypeInfo = returnValue.GetType().GetTypeInfo();
                    if (returnValueTypeInfo.IsGenericType && returnValueTypeInfo.GetGenericTypeDefinition() == typeof(ValueTask<>))
                    {
                        await (dynamic)returnValue;
                    }
                    break;
            }
        }
    }
}