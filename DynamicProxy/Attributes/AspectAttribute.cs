using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DynamicProxy.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AspectAttribute(params Type[] interceptorTypes) : Attribute
    {
        public IEnumerable<Type> InterceptorTypes => interceptorTypes;
    }

    public class AspectAttribute<T>() : AspectAttribute(typeof(T)) where T : IInterceptor;

    public class AspectAttribute<T1, T2>() : AspectAttribute(typeof(T1), typeof(T2)) 
        where T1 : IInterceptor 
        where T2 : IInterceptor;
    
    public class AspectAttribute<T1, T2, T3>() : AspectAttribute(typeof(T1), typeof(T2), typeof(T3)) 
        where T1 : IInterceptor 
        where T2 : IInterceptor
        where T3 : IInterceptor;
    
    public class AspectAttribute<T1, T2, T3, T4>() : AspectAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4)) 
        where T1 : IInterceptor 
        where T2 : IInterceptor
        where T3 : IInterceptor
        where T4 : IInterceptor;
    
    public class AspectAttribute<T1, T2, T3, T4, T5>() : AspectAttribute(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5)) 
        where T1 : IInterceptor 
        where T2 : IInterceptor
        where T3 : IInterceptor
        where T4 : IInterceptor
        where T5 : IInterceptor;
}