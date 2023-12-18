using Microsoft.EntityFrameworkCore;
using Toyer.Data.Entities;

namespace Toyer.Data.Context;

public class ToyerDbContext(DbContextOptions<ToyerDbContext> options) : DbContext(options)
{
    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceType> DeviceTypes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<PersonalInfo> PersonalInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.PersonalInfo)
            .WithOne(pi => pi.User)
            .HasForeignKey<PersonalInfo>(pi => pi.UserId);

        modelBuilder.Entity<PersonalInfo>()
            .HasOne(pi => pi.Address)
            .WithOne(a => a.PersonalInfo)
            .HasForeignKey<Address>(a => a.PersonalInfoId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Devices)
            .WithOne(d => d.User)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.SetNull);

    }
}
