using System;

namespace DynamicProxy.Extensions
{
    public static class InterceptorExtension
    {
        public static AspectBuilder AddInterceptor(this AspectBuilder builder, IInterceptor interceptor)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (interceptor == null) throw new ArgumentNullException(nameof(interceptor));

            return builder.AddAspect(next => interceptor.InvokeAsync);
        }
    }
}