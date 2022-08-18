using System.Reflection;
using System.Threading.Tasks;

namespace DynamicProxy.Extensions
{
    public static class ReturnTaskAwaitExtension
    {
        public static async ValueTask AwaitReturnAsync(this AspectContext context)
        {
            switch (context.ReturnValue)
            {
                case null:
                    break;
                case Task task:
                    await task;
                    break;
                case ValueTask valueTask:
                    await valueTask;
                    break;
                default:
                    var returnValueTypeInfo = context.ReturnValue.GetType().GetTypeInfo();
                    if (returnValueTypeInfo.IsGenericType 
                        && (returnValueTypeInfo.GetGenericTypeDefinition() == typeof(ValueTask<>) || returnValueTypeInfo.GetGenericTypeDefinition() == typeof(Task<>)))
                    {
                        await (dynamic)context.ReturnValue;
                    }
                    break;
            }
        }
    }
}