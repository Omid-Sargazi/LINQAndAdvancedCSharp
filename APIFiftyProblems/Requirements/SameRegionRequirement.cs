using Microsoft.AspNetCore.Authorization;

namespace APIFiftyProblems.Requirements
{
    public class SameRegionRequirement : IAuthorizationRequirement
    { }

    public class SameRegionHandler : AuthorizationHandler<SameRegionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameRegionRequirement requirement)
        {
            

            var userRegion = context.User.FindFirst("rigion")?.Value;
            if (userRegion == null)
            {
                return Task.CompletedTask;
            }

            if (context.Resource is DefaultHttpContext httpContext)
            {
                var routeRegion = httpContext.Request.RouteValues["region"]?.ToString();

                if (routeRegion != null && routeRegion.Equals(userRegion, StringComparison.OrdinalIgnoreCase))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}