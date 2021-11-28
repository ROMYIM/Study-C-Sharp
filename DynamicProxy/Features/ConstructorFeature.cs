using System.Reflection;
using System.Reflection.Emit;

namespace DynamicProxy.Features
{
    internal class ConstructorFeature
    {
        internal ConstructorInfo ConstructorInfo { get; set; }

        internal ConstructorBuilder ConstructorBuilder { get; set; }
        
        internal object?[]? Parameters { get; set; }

        internal object OriginalInstance { get; set; }
    }
}