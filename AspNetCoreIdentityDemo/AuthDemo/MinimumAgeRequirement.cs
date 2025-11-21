using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AuthDemo
{
    public class MinimumAgeRequirement:IAuthorizationRequirement
    {
        public int MinAge {get;set;}
        public MinimumAgeRequirement(int aeg)
        {
            MinAge = aeg;
        }
    }

    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var dobClaim = context.User.FindFirst(ClaimTypes.DateOfBirth);
            if(dobClaim==null)
            {
                return Task.CompletedTask;
            }

            if(!DateTime.TryParse(dobClaim.Value, out var dob))
            {
                return Task.CompletedTask;
            }

              var age = DateTime.Today.Year - dob.Year;
            if (dob > DateTime.Today.AddYears(-age))
            {
                age--;
            }

              if (age >= requirement.MinAge)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
        }
    }
}