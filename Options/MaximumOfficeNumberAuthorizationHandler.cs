// A handler that can determine whether a MaximumOfficeNumberRequirement is satisfied
using System.Collections;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

internal class MaximumOfficeNumberAuthorizationHandler : AuthorizationHandler<MaximumOfficeNumberRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaximumOfficeNumberRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Value == "admin1"))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

// A custom authorization requirement which requires office number to be below a certain value
internal class MaximumOfficeNumberRequirement : IAuthorizationRequirement
{
    public MaximumOfficeNumberRequirement(string role)
    {
        MaximumOfficeNumber = role;
    }

    public string MaximumOfficeNumber { get; private set; }
}