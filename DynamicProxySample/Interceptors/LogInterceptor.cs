using DynamicProxy;
using DynamicProxy.Extensions;
using Microsoft.Extensions.Logging;

namespace DynamicProxySample.Interceptors;

public class LogInterceptor : IInterceptor
{
    private readonly ILogger<LogInterceptor> _logger;

    public LogInterceptor(ILogger<LogInterceptor> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(AspectContext context, AspectDelegate next)
    {
        var argument = context.Parameters?[0];
        _logger.LogInformation("thread id {}", Environment.CurrentManagedThreadId);
        var trulyReturnValue = await context.GetTrulyReturnValueAsync();
        _logger.LogInformation("next before: argument is {Argument}, return value is {ReturnValue}", argument, trulyReturnValue);
        
        await next(context);
        
        _logger.LogInformation("thread id {}", Environment.CurrentManagedThreadId);
        _logger.LogInformation("next after: argument is {Argument}, return value is {ReturnValue}", argument, await context.GetTrulyReturnValueAsync());
    }
}