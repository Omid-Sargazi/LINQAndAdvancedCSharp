namespace RegionBasedPrivacySecurity.Middlewares
{
    public class RequestLoggingAndAuthMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestLoggingAndAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var nowUtc = DateTime.UtcNow;
            var method = context.Request.Method;
            var path = context.Request.Path;

            string clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                var xff = context.Request.Headers["X-Forwarded-For"].ToString();
                if (!string.IsNullOrEmpty(xff)) clientIp = xff.Split(',')[0].Trim();
            }

            Console.WriteLine($"[Request] {nowUtc:O} {clientIp} {method} {path}");

            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json; charset=utf-8";
                await context.Response.WriteAsync("{\"error\":\"Authorization header missing\"}");
                Console.WriteLine($"[Auth] 401 returned because Authorization header missing for {path}");
                return;
            }

            await _next(context);

        }

    }

    public static class RequestLoggingAndAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLoggingAndAuth(this IApplicationBuilder builder)


        {
            return builder.UseMiddleware<RequestLoggingAndAuthMiddleware>();
        }
    }
}
    
    
