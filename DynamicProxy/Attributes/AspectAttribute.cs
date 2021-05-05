using System.Threading.Tasks;

namespace DynamicProxy.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public abstract class AspectAttribute : System.Attribute
    {
        
        
        
        public abstract Task InvokeAsync(AspectContext context);
        
    }
}