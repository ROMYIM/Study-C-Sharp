using System.Linq.Expressions;
using System.Reflection;

namespace DynamicProxy
{
    public class AspectContext
    {

        public IServiceProvider ApplicationServices { get; }

        public object?[] Parameters { get; internal set; }

        public object? ReturnValue { get; internal set; }

        public MethodInfo? Method { get; internal init; }

        public object? Instance { get; internal init; }

        public bool InstanceMethodExecuted { get; internal set; }

        internal delegate object? MethodExecute(object target, object?[]? parameters);
 
        private delegate void VoidMethodExecute(object target, object?[]? parameters);
        
        internal MethodExecute? InvokeInstanceMethod { get; set; }

        internal AspectContext(IServiceProvider serviceProvider)
        {
            ApplicationServices = serviceProvider;
        }

        internal void GenerateInvokeMethod()
        {
            if (Method is null)
            {
                throw new InvalidOperationException("the methodInfo is null");
            }

            var parameterInfos = Method.GetParameters();
            if (parameterInfos.Any(info => info.ParameterType.IsByRef))
            {
                return;
            }
            InvokeInstanceMethod = BuildExecuteMethod<object>(Method, parameterInfos);
        }
        
        private static (MethodCallExpression, ParameterExpression[]?) BuildLambdaExpressionParameters<T>(MethodInfo methodInfo, ParameterInfo[] parameters)
        {
            
            var instanceType = typeof(T);

            var parametersArg = Expression.Parameter(typeof(object[]), "parameters");
            var instanceArg = Expression.Parameter(instanceType, "instance");

            var methodArguments = new Expression[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                var parameterType = parameters[i].ParameterType;
                if (parameterType.IsByRef)
                {
                    // var getParameterCall = Expression.ArrayIndex(parametersArg, Expression.Constant(i));
                    // methodArguments[i] = Expression.Convert(getParameterCall, parameterType.GetElementType()!);
                    throw new NotSupportedException("delegate is not supported to reference argument");
                }

                else
                {
                    var getParameterCall = Expression.ArrayIndex(parametersArg, Expression.Constant(i));
                    methodArguments[i] = Expression.Convert(getParameterCall, parameterType);
                }
            }

            Expression originalInstanceArg = instanceArg;
            if (methodInfo.DeclaringType != null && methodInfo.DeclaringType != typeof(T))
            {
                originalInstanceArg = Expression.Convert(originalInstanceArg, methodInfo.DeclaringType);
            }

            var instanceMethodCall = Expression.Call(originalInstanceArg, methodInfo, methodArguments);

            return (instanceMethodCall, new[] {instanceArg, parametersArg});
        }
        
        private static MethodExecute BuildExecuteMethod<T>(MethodInfo methodInfo, ParameterInfo[] parameters)
        {
            var (instanceMethodCall, parametersArg) = BuildLambdaExpressionParameters<T>(methodInfo, parameters);
            
            if (instanceMethodCall.Type == typeof(void))
            {
                var lambda = Expression.Lambda<VoidMethodExecute>(instanceMethodCall, parametersArg);
                var voidExecute = lambda.Compile();
                return WrapVoidMethod(voidExecute);
            }
            else
            {
                // must coerce methodCall to match ActionExecutor signature
                var castMethodCall = Expression.Convert(instanceMethodCall, typeof(object));
                var lambda = Expression.Lambda<MethodExecute>(castMethodCall, parametersArg);
                return lambda.Compile();
            }
        }
        
        private static MethodExecute WrapVoidMethod(VoidMethodExecute executor)
        {
            return delegate (object target, object?[]? parameters)
            {
                executor(target, parameters);
                return null;
            };
        }
    }
}