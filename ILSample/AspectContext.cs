using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace ILSample
{
    public delegate Task AspectDelegate(AspectContext context);
    
    public class AspectContext
    {

        public object?[]? Parameters { get; internal set; }

        public object ReturnValue { get; set; }

        public MethodInfo Method { get; internal set; }

        public object Instance { get; internal set; }

        internal AspectContext()
        {
            Method = typeof(Person).GetMethod("Show", BindingFlags.Public);
        }


    }
}