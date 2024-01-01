using Microsoft.EntityFrameworkCore;
using Toyer.Data.Entities;
using Toyer.Data.Extensions;

namespace Toyer.Data.Context;

public class ToyerDbContext(DbContextOptions<ToyerDbContext> options) : DbContext(options)
{
    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceType> DeviceTypes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<PersonalInfo> PersonalInfos { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ModelBuilderExtension.ConfigureToyerDbContextModel(modelBuilder);
    }
}
