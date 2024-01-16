using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Toyer.Logic.Services.Authorization.AuthorizationHandlers;

public static class PermissionRequirements
{
    public static OperationAuthorizationRequirement ReadPermission =
        new OperationAuthorizationRequirement() { Name = nameof(ReadPermission) };

    public static OperationAuthorizationRequirement EditPermission =
        new OperationAuthorizationRequirement() { Name = nameof(EditPermission) };

    public static OperationAuthorizationRequirement DeletePermission =
        new OperationAuthorizationRequirement() { Name = nameof(DeletePermission) };
}

