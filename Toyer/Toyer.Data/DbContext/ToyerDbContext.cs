

using Microsoft.EntityFrameworkCore;
using Toyer.Data.Entities;

namespace Toyer.Data.dbContext;

public class ToyerDbContext : DbContext
{
    public ToyerDbContext(DbContextOptions connectionOptions) : base(connectionOptions)
    {
    }

    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceType> DeviceTypes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<PersonalInfo> PersonalInfos { get; set; }



}
