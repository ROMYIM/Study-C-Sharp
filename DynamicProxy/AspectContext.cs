using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using DynamicProxy.Attributes;

namespace DynamicProxy
{
    public class AspectContext
    {
        public IReadOnlyCollection<AspectAttribute> Aspectes { get; set; }
    
        public IReadOnlyCollection<object> Parameters { get; private set; }

        public object ReturnValue { get; private set; }

        public MethodInfo Method { get; private set; }

        public object Instance { get; private set; }

        public AspectContext(object instance, object[] parameters, MethodInfo method)
        {
            Instance = instance;
            Parameters = new ReadOnlyCollection<object>(parameters);
            Method = method;
        }

        public void Invoke()
        {
            ReturnValue = Method.Invoke(Instance, Parameters.ToArray());
        }
    }
}