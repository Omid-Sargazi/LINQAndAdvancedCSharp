

using System.Diagnostics;

namespace ECommerceGatewayAPI.Middlewares
{
    public class ECommerceMiddleware
    {
        private readonly RequestDelegate _next;
        public ECommerceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();



            sw.Stop();
            if (sw.ElapsedMilliseconds >= 1000)
            {
                Console.WriteLine("Time is  high");
                return;
            }

            if (!context.Request.Headers.ContainsKey("X-Api-Key"))
            {
                // context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.StatusCode = 401;
                return;
            }
                await _next(context);
        }
    }

    public static class ECommerceMiddlewareExtention
    {
        public static IApplicationBuilder UseECommerceMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ECommerceMiddleware>();
        }
    }
}