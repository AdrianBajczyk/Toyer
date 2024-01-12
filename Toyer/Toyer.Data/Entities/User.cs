using Microsoft.AspNetCore.Identity;

namespace Toyer.Data.Entities;

public class User : IdentityUser
{
    public DateTime AccCreationDate { get; set; } = DateTime.Now;

    public List<string> DevicesFKs { get; set; } = new();
    public PersonalInfo? PersonalInfo { get; set; }
}
