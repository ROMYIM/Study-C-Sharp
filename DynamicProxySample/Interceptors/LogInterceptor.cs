using DynamicProxy;
using DynamicProxy.Extensions;
using Microsoft.Extensions.Logging;

namespace DynamicProxySample.Interceptors;

public class LogInterceptor(ILogger<LogInterceptor> logger) : IInterceptor
{
    public async Task InvokeAsync(AspectContext context, AspectDelegate next)
    {
        var argument = context.Parameters?[0];
        logger.LogInformation("thread id {}", Environment.CurrentManagedThreadId);
        var trulyReturnValue = await context.GetTrulyReturnValueAsync();
        logger.LogInformation("next before: argument is {Argument}, return value is {ReturnValue}", argument, trulyReturnValue);
        
        await next(context);
        
        logger.LogInformation("thread id {}", Environment.CurrentManagedThreadId);
        logger.LogInformation("next after: argument is {Argument}, return value is {ReturnValue}", argument, await context.GetTrulyReturnValueAsync());
    }
}