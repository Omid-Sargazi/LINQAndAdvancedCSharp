using BimeDotCom.Models;
using Microsoft.AspNetCore.Authorization;

namespace BimeDotCom.Requirements
{
    public class SameRegionRequirement : IAuthorizationRequirement
    { }

    public class SameRegionHandler : AuthorizationHandler<SameRegionRequirement>
    {
        private readonly IHttpContextAccessor _http;
        public SameRegionHandler(IHttpContextAccessor http)
        {
            _http = http;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameRegionRequirement requirement)
        {
            var httpContext = _http.HttpContext;
            if (httpContext == null) return Task.CompletedTask;

            var userRegion = context.User.FindFirst("region")?.Value;
            var routeRegion = httpContext.GetRouteValue("region")?.ToString();

            if (userRegion != null && routeRegion != null && User.Equals(routeRegion, StringComparison.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}