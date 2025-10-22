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
            try
            {
                timer.Start();
                await Task.Delay(1000);
                await _next(context);
            }
            catch (Exception ex) 
            {

                throw new Exception("There is an error");
            }

            finally
            {
                timer.Stop();
                Console.WriteLine($"Elapsed: {timer.ElapsedMilliseconds} ms");
            }


        }
    }
}