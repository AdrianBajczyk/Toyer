using Microsoft.EntityFrameworkCore;
using Toyer.Data.Context;

namespace Toyer.API.Extensions.WebAppBuilder;

public static class DbContextServicesExtensions
{
    public static IServiceCollection AddCustomDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ToyerDbContext>(options => options.UseSqlServer(configuration["ToyerAzureSqlConnectionstring"]));
        services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(configuration["ToyerIdentityAzureSqlConnectionstring"]));

        return services;
    }
}
