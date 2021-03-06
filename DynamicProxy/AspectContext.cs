using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DynamicProxy.Attributes;

namespace DynamicProxy
{
    public abstract class AspectContext
    {
        private List<Func<AspectDelegate, AspectDelegate>> _aspects;
        public IReadOnlyCollection<Func<AspectDelegate, AspectDelegate>> Aspects => _aspects;
        

        public IReadOnlyCollection<object> Parameters { get; private set; }

        public object ReturnValue { get; private set; }

        public MethodInfo Method { get; private set; }

        public object Instance { get; private set; }

        public AspectContext(object instance, object[] parameters, MethodInfo method)
        {
            Instance = instance;
            Parameters = new ReadOnlyCollection<object>(parameters);
            Method = method;
            _aspects = new List<AspectAttribute>();
            
        }

        internal void AddAspect(AspectAttribute aspect)
        {
            if (aspect == null) return;
            _aspects.Add(aspect);
        }

        public abstract Task InvokeAsync(AspectDelegate aspectDelegate);
    }
}