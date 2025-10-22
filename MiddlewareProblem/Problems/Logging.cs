using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;

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
           
            await _next.Invoke(context);
           
        }
    }

    public class RequestTimingMiddleware
    {

        private readonly RequestDelegate _netx;
        private readonly ILogger<RequestTimingMiddleware> _logger;
        public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
        {
            _netx = next;
            _logger = logger;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await Task.Delay(500);

            await _netx(context);
            stopwatch.Stop();


            await Task.Delay(500);
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }


    public class RequestTimingMiddleware2
    {
        private readonly RequestDelegate _next;
        public RequestTimingMiddleware2(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

                Stopwatch timer = new Stopwatch();
                timer.Start();
            try
            {
                await Task.Delay(200);
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "appliction/json";

                var errorResponse = new
                {
                    success = false,
                    error = ex.Message,
                    path = context.Request.Path,
                    method = context.Request.Method
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);

                throw new Exception("There is an error",ex);
            }

            finally
            {
                timer.Stop();
                Console.WriteLine($"Elapsed: {timer.ElapsedMilliseconds} ms");

                Console.WriteLine($"Request[{context.Request.Method}{context.Request.Path} took {timer.ElapsedMilliseconds}ms]");

                if(!context.Response.HasStarted)
                    context.Response.Headers["X-Elapsed-Time"] = $"{timer.ElapsedMilliseconds}";
            }


        }
    }
}