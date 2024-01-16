using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using Toyer.Data.Entities;

public static class UserManagerExtensions
{
    public static async Task<bool> IsInAnyRoleAsync(this UserManager<User> userManager, User user, [NotNull] params string[] roles)
    {
        foreach (var role in roles)
        {
            if (await userManager.IsInRoleAsync(user, role))
            {
                return true;
            }
        }
        return false;
    }
}