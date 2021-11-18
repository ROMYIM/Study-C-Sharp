using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MiddleWareSample.MiddleWares
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger _logger;

        private readonly Guid _id;

        public TestMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _next = next;
            _id = Guid.NewGuid();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("test middle ware id {}", _id);
            await _next(context);
        }
    }
}