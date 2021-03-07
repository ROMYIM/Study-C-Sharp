using System.Threading.Tasks;

namespace WebHostSample.Http
{
    public delegate Task RequestDelegate(HttpContext context);
}