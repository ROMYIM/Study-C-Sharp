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
    public class AspectContext
    {

        public IServiceProvider ApplicationServices { get; }

        public object?[]? Parameters { get; internal set; }

        public object ReturnValue { get; set; }

        public MethodInfo Method { get; internal set; }

        public object Instance { get; internal set; }

        internal AspectContext(IServiceProvider serviceProvider)
        {
            ApplicationServices = serviceProvider;
        }


    }
}