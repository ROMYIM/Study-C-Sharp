using System.Reflection;
using System.Reflection.Emit;

namespace ILSample;

public class MethodFeature
{
    public MethodInfo MethodInfo { get; set; }

    public MethodBuilder MethodBuilder { get; set; }
        
    public object?[]? Parameters { get; set; }
        
    public object OriginalInstance { get; set; }
}