using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace Toyer.Logic.Services.Authorization.AuthorizationHandlers;


public class PermissionHandler : AuthorizationHandler<OperationAuthorizationRequirement, string>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, string userIdResource)
    {
        switch (requirement.Name)
        {
            case nameof(PermissionRequirements.EditPermission):
                {
                    if (IsOwner(context.User, userIdResource) || IsPrivileged(context.User))
                    {
                        context.Succeed(requirement);
                    }
                    break;
                }
            case nameof(PermissionRequirements.ReadPermission):
                {
                    if (IsOwner(context.User, userIdResource) || IsPrivileged(context.User))
                    {
                        context.Succeed(requirement);
                    }
                    break;
                }
            case nameof(PermissionRequirements.DeletePermission):
                {
                    if (IsOwner(context.User, userIdResource) || IsPrivileged(context.User))
                    {
                        context.Succeed(requirement);
                    }
                    break;
                }
        }
        return Task.CompletedTask;
    }

    private static bool IsOwner(ClaimsPrincipal user, string userIdResource)
    {
            return user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value == userIdResource; 
    }

    private static bool IsPrivileged(ClaimsPrincipal user)
    {
        return (user.IsInRole("Administrator") || user.IsInRole("Employee"));
    }


}

