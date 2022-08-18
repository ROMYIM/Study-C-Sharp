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

        public IEnumerable<Type> InterceptorTypes => _interceptorsTypes;

        public AspectAttribute(params Type[] interceptorTypes)
        {
            if (interceptorTypes == null) return;
            _interceptorsTypes = new Type[interceptorTypes.Length];
            for (var i = 0; i < interceptorTypes.Length; i++)
            {
                if (typeof(IInterceptor).IsAssignableFrom(interceptorTypes[i]))
                    _interceptorsTypes[i] = interceptorTypes[i];
            }
        }

    }
}