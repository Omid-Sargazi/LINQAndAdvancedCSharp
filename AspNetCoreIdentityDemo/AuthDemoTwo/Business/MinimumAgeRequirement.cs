using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AuthDemoTwo.Business
{
    public class MinimumAgeRequirement:IAuthorizationRequirement
    {
        public int MinimumAge {get;}
        public MinimumAgeRequirement(int age)
        {
            MinimumAge = age;
        }
    }

    public class MinimumAgeHabdler : AuthorizationHandler<MinimumAgeRequirement>
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

              if (age >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
        }
    }
}