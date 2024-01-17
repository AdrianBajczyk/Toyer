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
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            //obczaj opcje 
        })
        .AddEntityFrameworkStores<UsersDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}

//rainbow attacks 