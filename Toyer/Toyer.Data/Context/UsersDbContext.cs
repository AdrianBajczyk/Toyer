using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Toyer.Data.Configurations;
using Toyer.Data.Entities;
using Toyer.Data.Extensions;

namespace Toyer.Data.Context;

public  class UsersDbContext(DbContextOptions<UsersDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<PersonalInfo> PersonalInfos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        UserModelBuilderExtension.ConfigureModelBuilder(modelBuilder);

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }
}
