using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Toyer.Data.Context;

public class ToyerContextFactory : IDesignTimeDbContextFactory<ToyerDbContext>
{
    public ToyerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ToyerDbContext>();
        optionsBuilder.UseSqlServer("Server=tcp:adrianbajczyk.database.windows.net,1433;Initial Catalog=ToyerPasswordless;Persist Security Info=False;User ID=Adrian_Bajczyk;Password=SkonfigurowanoNaAmen1221;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        return new ToyerDbContext(optionsBuilder.Options);
    }
}
