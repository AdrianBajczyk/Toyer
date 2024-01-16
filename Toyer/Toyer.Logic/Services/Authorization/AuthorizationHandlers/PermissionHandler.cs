using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Toyer.Data.Entities;

namespace Toyer.Logic.Services.Authorization.AuthorizationHandlers;

public class ReadPermission : IAuthorizationRequirement;
public class EditPermission : IAuthorizationRequirement;
public class DeletePermission : IAuthorizationRequirement;


public class PermissionHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        var pendingRequirements = context.PendingRequirements.ToList();

        foreach (var requirement in pendingRequirements)
        {
            if (requirement is ReadPermission)
            {
                if (context.HasSucceeded)
                {
                    context.Succeed(requirement);
                }
            }
            else if (requirement is EditPermission || requirement is DeletePermission)
            {
                if (IsOwner(context.User, context.Resource) || IsPrivileged(context.User))
                {
                    context.Succeed(requirement);
                }
            }
        }

        return Task.CompletedTask;
    }

    private static bool IsOwner(ClaimsPrincipal user, object? resource)
    {
        if(resource is string resourceUseId)
        {
            return user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value == resourceUseId;
        }
        throw new ArgumentException("Non userId passed as a resource argument.");
    }

    private static bool IsPrivileged(ClaimsPrincipal user)
    {
        return (user.IsInRole("Administrator") || user.IsInRole("Employee"));
    }
}

