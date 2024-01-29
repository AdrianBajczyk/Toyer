using Microsoft.AspNetCore.Identity;
using Toyer.Data.Context;
using Toyer.Data.Entities;

namespace Toyer.API.Extensions.WebAppBuilder;

public static class IdentityServicesExtensions
{
    public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options =>
        {
            SetPasswordConfiguration(options);
            SetLockoutConfiguration(options);
            options.SignIn.RequireConfirmedAccount = true;
            options.User.RequireUniqueEmail = true;

        })
        .AddEntityFrameworkStores<UsersDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }

    private static void SetLockoutConfiguration(IdentityOptions options)
    {
        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 3;
    }

    private static void SetPasswordConfiguration(IdentityOptions options)
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
    }
}

//rainbow attacks 