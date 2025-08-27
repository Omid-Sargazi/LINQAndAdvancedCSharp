namespace Middlewares.Middleware1
{
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate _next;
        public MyCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("before controller");
            await _next(context);
            Console.WriteLine("after controller");
        }


    }
}