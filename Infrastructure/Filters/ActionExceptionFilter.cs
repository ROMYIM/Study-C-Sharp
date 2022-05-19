using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;

namespace Infrastructure.Filters;

public class ActionExceptionFilter<T> : IAsyncExceptionFilter where T : ApiResult, new()
{
    private readonly ILogger _logger;

    private readonly ObjectPool<StringBuilder> _stringBuilderPool;

    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="loggerFactory">日志工厂</param>
    /// <param name="stringBuilderPool">stringBuilder对象池</param>
    public ActionExceptionFilter(ILoggerFactory loggerFactory, ObjectPool<StringBuilder> stringBuilderPool)
    {
        _stringBuilderPool = stringBuilderPool;
        _logger = loggerFactory.CreateLogger(GetType());
    }
    
    /// <summary>
    /// 请求异常处理。
    /// 只能处理controller域的异常。
    /// 中间件的异常需要ExceptionHandler
    /// </summary>
    /// <param name="context">异常处理上下文</param>
    /// <returns></returns>
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var ex = context.Exception;
        var displayName = context.ActionDescriptor.DisplayName;
        var traceId = Activity.Current?.TraceId.ToString() ?? context.HttpContext.TraceIdentifier;
        var eventId = new EventId(-1, traceId);

        var parameterTipsBuilder = _stringBuilderPool.Get();
        foreach (var parameter in context.ModelState)
        {
            parameterTipsBuilder.Append('@').Append(parameter.Key).Append('=').Append(parameter.Value.AttemptedValue)
                .Append(' ');
        }

        _logger.LogError(eventId, ex, "\n\t请求ID [{}]\n\t请求路径 [{}] \n\t请求参数 [{}]", traceId, displayName,
            parameterTipsBuilder.ToString());

        context.Result = new JsonResult(new T()
        {
            Data = ex.Data,
            Message = ex.Message,
            Success = false
        });

        context.ExceptionHandled = true;
        _stringBuilderPool.Return(parameterTipsBuilder);
        return Task.CompletedTask;
    }
}