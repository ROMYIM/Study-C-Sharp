using System;

namespace DynamicProxy.Extensions
{
    public static class InterceptorExtension
    {
        public static AspectBuilder AddInterceptor(this AspectBuilder builder, Type interceptorType)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(interceptorType);
            if (!typeof(IInterceptor).IsAssignableFrom(interceptorType))
            {
                throw new ArgumentException("the type is not assignable from 'IInterceptor'", nameof(interceptorType));
            }

            return builder.AddAspect(next =>
            {
                return async context =>
                {
                    if (context.ApplicationServices.GetService(interceptorType) is IInterceptor interceptor)
                    {
                        await interceptor.InvokeAsync(context, next);
                    }
                };
            });
        }
    }
}