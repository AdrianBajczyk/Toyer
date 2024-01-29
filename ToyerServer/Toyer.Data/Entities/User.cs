using Microsoft.AspNetCore.Identity;

namespace Toyer.Data.Entities;

public class User : IdentityUser
{
    public DateTime AccCreationDate { get; set; } = DateTime.Now;
    public PersonalInfo? PersonalInfo { get; set; }
    public RefreshTokenModel? RefreshTokenModel { get; set; }
}
