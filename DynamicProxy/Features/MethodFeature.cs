using System.Reflection;
using System.Reflection.Emit;

namespace DynamicProxy.Features
{
    internal class MethodFeature
    {
        internal MethodInfo MethodInfo { get; set; }

        internal object?[]? Parameters { get; set; }
        
        internal object OriginalInstance { get; set; }
    }
}