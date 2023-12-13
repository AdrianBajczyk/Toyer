using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Toyer.Data.Context;

public class ToyerContextFactory : IDesignTimeDbContextFactory<ToyerDbContext>

{
    private readonly IConfiguration _config;

    public ToyerContextFactory(IConfiguration config)
    {
        _config = config;
    }


    public ToyerDbContext CreateDbContext(string[] args)
    {


        var optionsBuilder = new DbContextOptionsBuilder<ToyerDbContext>();
        optionsBuilder.UseSqlServer(_config["Azure"]);

        return new ToyerDbContext(optionsBuilder.Options);
    }
}
