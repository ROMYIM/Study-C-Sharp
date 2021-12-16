using System.Reflection;
using System.Threading.Tasks;

namespace DynamicProxy.Extensions
{
    public static class ReturnTaskAwaitExtension
    {
        public static async ValueTask AwaitReturnAsync(this AspectContext context, object returnValue)
        {
            switch (returnValue)
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
                    var returnValueTypeInfo = returnValue.GetType().GetTypeInfo();
                    if (returnValueTypeInfo.IsGenericType && returnValueTypeInfo.GetGenericTypeDefinition() == typeof(ValueTask<>))
                    {
                        await (dynamic)returnValue;
                    }
                    break;
            }
        }
    }
}