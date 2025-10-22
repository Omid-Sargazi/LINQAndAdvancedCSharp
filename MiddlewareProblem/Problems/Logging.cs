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
        private readonly ILogger<RequestTimingMiddleware2> _logger;
        public RequestTimingMiddleware2(RequestDelegate next,ILogger<RequestTimingMiddleware2> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

                Stopwatch timer = new Stopwatch();
                timer.Start();
            try
            {
                _logger.LogInformation("Incomming Request{Method},{Path}", context.Request.Method, context.Request.Path);
                await Task.Delay(200);
                await _next(context);
                timer.Stop();

                _logger.LogInformation("Completed {Method}{Path} with {Status Code} in Elapse ms", context.Request.Method, context.Request.Path,

                context.Response.StatusCode,timer.ElapsedMilliseconds
                );
            }
            catch (Exception ex)
            {




                _logger.LogError("Execution on {Method},{Path} aftre {Elapsed} ms", context.Request.Method, context.Request.Path, timer.ElapsedMilliseconds);
                 context.Response.StatusCode = 500;
                context.Response.ContentType = "appliction/json";

                 await context.Response.WriteAsync($"{{\"error\":\"{ex.Message}\"}}");
            }

            finally
            {
                Console.WriteLine("Finnalllllly");
            }


        }
    }
}