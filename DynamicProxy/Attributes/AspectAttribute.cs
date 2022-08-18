using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace DynamicProxy.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AspectAttribute : Attribute
    {
        private readonly Type[] _interceptorsTypes;

        public IReadOnlyList<Type> InterceptorTypes => _interceptorsTypes;

        public AspectAttribute(params Type[] interceptorTypes)
        {
            if (interceptorTypes != null)
            {
                _interceptorsTypes = new Type[interceptorTypes.Length];
                for (int i = 0; i < interceptorTypes.Length; i++)
                {
                    if (interceptorTypes[i].GetTypeInfo().IsAssignableFrom(typeof(IInterceptor)))
                        _interceptorsTypes[i] = interceptorTypes[i];
                }
            }
        }

    }
}