namespace MiddlewareProblem.Problems
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }

    }
    public class Logging
    {
        private readonly RequestDelegate _next;
        public Logging(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"Request{context.Request.Path}");
            await _next.Invoke(context);
            Console.WriteLine($"Resposne : {context.Response.Body}");
        }
    }

    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _netx;
        public RequestTimingMiddleware(RequestDelegate next)
        {
            _netx = next;
        }
    }
}