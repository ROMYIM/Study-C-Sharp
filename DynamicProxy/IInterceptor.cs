﻿namespace DynamicProxy
{
    public interface IInterceptor
    {
        Task InvokeAsync(AspectContext context, AspectDelegate next);
    }
}