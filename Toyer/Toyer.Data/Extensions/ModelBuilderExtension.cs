using Microsoft.EntityFrameworkCore;
using Toyer.Data.Entities;

namespace Toyer.Data.Extensions;

public static class ModelBuilderExtension
{
    public static void ConfigureToyerDbContextModel(this ModelBuilder modelBuilder)
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
