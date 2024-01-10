using Microsoft.AspNetCore.Identity;

namespace Toyer.Data.Entities;

public class User : IdentityUser
{
    public DateTime AccCreationDate { get; set; } = DateTime.Now;

    public ICollection<string> DevicesFKs { get; set; } = new HashSet<string>();
    public PersonalInfo? PersonalInfo { get; set; }
}
