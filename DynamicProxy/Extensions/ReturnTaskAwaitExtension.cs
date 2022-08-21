using System;
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
                    if (returnValueTypeInfo.IsGenericType && returnValueTypeInfo.GetGenericTypeDefinition() == typeof(ValueTask<>))
                    {
                        await (dynamic)context.ReturnValue;
                    }
                    break;
            }
        }

        public static bool IsAsync(this AspectContext context, out bool hasTrulyReturnValue)
        {
            ArgumentNullException.ThrowIfNull(context.Method);
            var returnValueTypeInfo = context.Method.ReturnType.GetTypeInfo();
            var returnValueType = returnValueTypeInfo.AsType();

            if (!returnValueTypeInfo.IsGenericType)
            {
                hasTrulyReturnValue = returnValueType != typeof(void);
                return returnValueType == typeof(Task) || returnValueType == typeof(ValueTask);
            }
            
            var genericTypeDefinition = returnValueTypeInfo.GetGenericTypeDefinition();
            hasTrulyReturnValue = true;
            return genericTypeDefinition == typeof(ValueTask<>) || genericTypeDefinition == typeof(Task<>);
        }

        public static async Task<object?> GetTrulyReturnValueAsync(this AspectContext context)
        {
            if (!context.InstanceMethodExecuted)
            {
                throw new InvalidOperationException("The instance's method has not executed yet!");
            }

            if (context.IsAsync(out var hasTrulyReturnValue))
            {
                if (hasTrulyReturnValue) return await (dynamic) context.ReturnValue!;
                return null;
            }

            return hasTrulyReturnValue ? context.ReturnValue : null;
        }
    }
}