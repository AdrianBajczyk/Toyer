using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Toyer.Data.Context;

public class ToyerContextFactory : IDesignTimeDbContextFactory<ToyerDbContext>
{

    public ToyerDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets<ToyerContextFactory>() 
                .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ToyerDbContext>();
        var connectionString = configuration["AzureSqlConnectionstring"];
        Console.WriteLine($"Connection String: {connectionString}");
        optionsBuilder.UseSqlServer(connectionString);

        return new ToyerDbContext(optionsBuilder.Options);
    }
}
