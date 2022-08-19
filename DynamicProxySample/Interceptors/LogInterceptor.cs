using DynamicProxy;
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
        // Console.WriteLine($"next before: argument is {argument}, return value is {context.ReturnValue}");
        _logger.LogInformation("next before: argument is {Argument}, return value is {ReturnValue}", argument, context.ReturnValue);
        
        await next(context);
        
        // Console.WriteLine($"next after: argument is {argument}, return value is {context.ReturnValue}");
        _logger.LogInformation("next after: argument is {Argument}, return value is {ReturnValue}", argument, context.ReturnValue);
    }
}