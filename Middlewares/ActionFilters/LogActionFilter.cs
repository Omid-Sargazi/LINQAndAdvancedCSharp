using Microsoft.AspNetCore.Mvc.Filters;

namespace Middlewares.ActionFilters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"Action Started.");


        }
    }
}