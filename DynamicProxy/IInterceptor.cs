using System.Threading.Tasks;

namespace DynamicProxy
{
    public interface IInterceptor
    {
        Task InvokeAsync(AspectContext context);
    }
}