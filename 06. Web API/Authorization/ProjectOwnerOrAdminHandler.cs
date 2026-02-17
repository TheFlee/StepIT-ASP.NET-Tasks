using _06._Web_API.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace _06._Web_API.Authorization;

public class ProjectOwnerOrAdminHandler
    : AuthorizationHandler<ProjectOwnerOrAdminRequirment, Project>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ProjectOwnerOrAdminRequirment requirement, Project resource)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
            return Task.CompletedTask;

        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (context.User.IsInRole("Manager") && resource.OwnerId == userId)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
