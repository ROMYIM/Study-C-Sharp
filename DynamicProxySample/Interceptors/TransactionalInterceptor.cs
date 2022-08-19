using DynamicProxy;
using FreeSql;

namespace DynamicProxySample.Interceptors;

public class TransactionalInterceptor : IInterceptor
{
    private readonly UnitOfWorkManager _unitOfWorkManager;

    public TransactionalInterceptor(UnitOfWorkManager unitOfWorkManager)
    {
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task InvokeAsync(AspectContext context, AspectDelegate next)
    {
        var unitOfWork = _unitOfWorkManager.Begin();
        
        await next(context);

        var returnValue = context.ReturnValue.ToString();
        if (returnValue != "a") unitOfWork.Rollback();
        else unitOfWork.Commit();
    }
}