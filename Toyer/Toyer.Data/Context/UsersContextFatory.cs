using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Toyer.Data.Context;

public class UsersContextFatory : IDesignTimeDbContextFactory<UsersDbContext>
{
    public UsersDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets<UsersContextFatory>()
                .Build();

        var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>();
        var connectionString = configuration["ToyerIdentityLocalConnectionstring"];
        optionsBuilder.UseSqlServer(connectionString);

        return new UsersDbContext(optionsBuilder.Options);
    }
}