using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Toyer.Data.Context;

namespace Toyer.API.Extensions;

public static class DbContextServicesExtensions
{
    public static IServiceCollection AddCustomDbContexts(this IServiceCollection services)
    {
        var client = services.BuildServiceProvider().GetRequiredService<SecretClient>();

        services.AddDbContext<ToyerDbContext>(options => options.UseSqlServer(client.GetSecret("ToyerAzureSqlConnectionstring").Value.Value.ToString()));
        services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(client.GetSecret("ToyerIdentityAzureSqlConnectionstring").Value.Value.ToString()));

        return services;
    }
}
