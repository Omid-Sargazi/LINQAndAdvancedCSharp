namespace Middlewares.Middleware1
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($" received request:  {context.Request.Path}");
            await _next(context);
            Console.WriteLine($"response received: {context.Response.StatusCode}");
        }
    }
}