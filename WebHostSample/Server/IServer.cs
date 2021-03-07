using System.Threading.Tasks;
using WebHostSample.Http;

namespace WebHostSample.Server
{
    public interface IServer
    {
        Task StartAsync(RequestDelegate handler);
    }
}