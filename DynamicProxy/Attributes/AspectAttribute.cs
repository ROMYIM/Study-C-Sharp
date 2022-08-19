using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DynamicProxy.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AspectAttribute : Attribute
    {
        private readonly List<Type> _interceptorsTypes;

        public IEnumerable<Type> InterceptorTypes => _interceptorsTypes;

        public AspectAttribute(params Type[] interceptorTypes)
        {
            if (!interceptorTypes.Any()) return;
            _interceptorsTypes = new List<Type>(interceptorTypes.Length);
            Array.ForEach(interceptorTypes, interceptorType => _interceptorsTypes.Add(interceptorType));
        }

    }
}