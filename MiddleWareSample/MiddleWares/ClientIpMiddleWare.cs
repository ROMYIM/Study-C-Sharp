using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MiddleWareSample.MiddleWares
{
    public class ClientIpMiddleWare : IMiddleware
    {
        private readonly ILogger _logger;

        private const int step = 5000;

        private static int index = 0;

        public ClientIpMiddleWare(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType());
        }

        async Task IMiddleware.InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var ipAddress = context.Connection.RemoteIpAddress;
            var port = context.Connection.RemotePort;

            _logger.LogInformation("请求地址：{}:{}", ipAddress, port);

            await next(context);

            var currentIndex = Interlocked.Add(ref index, step);
            _logger.LogInformation("计数：{}", currentIndex);

            await context.Response.WriteAsync(currentIndex.ToString());
        }
    }
}