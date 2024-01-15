using Microsoft.EntityFrameworkCore;
using Toyer.API.Controllers;
using Toyer.Data.Context;
using Toyer.Data.Mappings;
using Toyer.Logic.Mappings.UserMappings.classes;
using Toyer.Logic.Mappings.UserMappings.Classes;
using Toyer.Logic.Services.DeviceMessaging;
using Toyer.Logic.Services.DeviceProvisioningService;
using Toyer.Logic.Services.Repositories.Classes;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Extensions.WebAppBuilder;

internal static class DbContextServicesExtensions
{
    internal static IServiceCollection AddCustomDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ToyerDbContext>(options => options.UseSqlServer(configuration["ToyerLocalConnectionstring"]));
        services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(configuration["ToyerIdentityLocalConnectionstring"]));

        return services;
    }
}
