using System;
using System.Reflection;

namespace ILSample
{
    public class AspectContext
    {

        public IServiceProvider ApplicationServices { get; }

        public object?[]? Parameters { get; internal set; }

        public object? ReturnValue { get; set; }

        public MethodInfo Method { get; internal set; }

        public object Instance { get; internal set; }

        internal AspectContext(IServiceProvider serviceProvider)
        {
            ApplicationServices = serviceProvider;
        }


    }
}