namespace RegionBasedPrivacySecurity.Middlewares
{
    public class CheckItemMiddleware
    {
        private readonly RequestDelegate _next;
        public CheckItemMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                 await _next.Invoke(context);
                Console.WriteLine("There is Middleware");
            }
            catch (System.Exception)
            {
                
                throw;
            }
           
        }
    }
}