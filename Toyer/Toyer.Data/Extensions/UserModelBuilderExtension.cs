using Microsoft.EntityFrameworkCore;
using Toyer.Data.Entities;

namespace Toyer.Data.Extensions;

public static class UserModelBuilderExtension
{
    public static void ConfigureModelBuilder(this ModelBuilder modelBuilder)
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
                .HasOne(u => u.RefreshTokenModel)       
                .WithOne(rt => rt.User)                  
                .HasForeignKey<RefreshTokenModel>(rt => rt.UserId)  
                .OnDelete(DeleteBehavior.Cascade);

    }
}
