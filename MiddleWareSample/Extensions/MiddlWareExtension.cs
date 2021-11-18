

using Microsoft.AspNetCore.Builder;
using MiddleWareSample.MiddleWares;

namespace MiddleWareSample.Extensions
{
    public static class MiddlWareExtension
    {
        public static IApplicationBuilder UseTest(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TestMiddleware>();
        }
    }
}