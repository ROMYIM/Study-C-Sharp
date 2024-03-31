using DynamicProxy;
using DynamicProxy.Extensions;
using FreeSql;
using Microsoft.Extensions.Logging;

namespace DynamicProxySample.Interceptors;

public class TransactionalInterceptor(UnitOfWorkManager unitOfWorkManager, ILogger<TransactionalInterceptor> logger)
    : IInterceptor
{
    public async Task InvokeAsync(AspectContext context, AspectDelegate next)
    {
        logger.LogInformation("thread id {}", Environment.CurrentManagedThreadId);
        logger.LogInformation("start transaction");   
        var unitOfWork = unitOfWorkManager.Begin();
        
        await next(context);

        logger.LogInformation("thread id {}", Environment.CurrentManagedThreadId);
        
        var returnValue = (await context.GetTrulyReturnValueAsync())?.ToString();
        if (returnValue != "a")
        {
            logger.LogInformation("transaction rollback");
            unitOfWork.Rollback();
        }
        else
        {
            logger.LogInformation("transaction commit");
            unitOfWork.Commit();
        }
    }
}