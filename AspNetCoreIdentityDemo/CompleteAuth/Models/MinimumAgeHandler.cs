using Microsoft.AspNetCore.Authorization;

namespace CompleteAuth.Models
{
   public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var dobClaim = context.User.FindFirst("DateOfBirth");
        if (dobClaim != null && DateTime.TryParse(dobClaim.Value, out var dob))
        {
            var age = DateTime.Today.Year - dob.Year;
            if (dob > DateTime.Today.AddYears(-age)) age--;
            if (age >= requirement.MinimumAge)
                context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}

public class MinimumAgeRequirement : IAuthorizationRequirement
{
    public int MinimumAge { get; }
    public MinimumAgeRequirement(int age) => MinimumAge = age;
}

public class RegionHandler : AuthorizationHandler<RegionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RegionRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == "Region" && c.Value == requirement.AllowedRegion))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}

public class RegionRequirement : IAuthorizationRequirement
{
    public string AllowedRegion { get; }
    public RegionRequirement(string region) => AllowedRegion = region;
}
}