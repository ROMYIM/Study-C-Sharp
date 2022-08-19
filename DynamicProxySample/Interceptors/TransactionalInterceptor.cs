using DynamicProxy;
using FreeSql;
using Microsoft.Extensions.Logging;

namespace DynamicProxySample.Interceptors;

public class TransactionalInterceptor : IInterceptor
{
    private readonly UnitOfWorkManager _unitOfWorkManager;

    private readonly ILogger<TransactionalInterceptor> _logger;

    public TransactionalInterceptor(UnitOfWorkManager unitOfWorkManager, ILogger<TransactionalInterceptor> logger)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _logger = logger;
    }

    public async Task InvokeAsync(AspectContext context, AspectDelegate next)
    {
        _logger.LogInformation("start transaction");   
        var unitOfWork = _unitOfWorkManager.Begin();
        
        await next(context);

        var returnValue = context.ReturnValue.ToString();
        if (returnValue != "a")
        {
            _logger.LogInformation("transaction rollback");
            unitOfWork.Rollback();
        }
        else
        {
            _logger.LogInformation("transaction commit");
            unitOfWork.Commit();
        }
    }
}