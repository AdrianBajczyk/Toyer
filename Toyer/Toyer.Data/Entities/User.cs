using System.ComponentModel.DataAnnotations;

namespace Toyer.Data.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public DateTime AccCreationDate { get; set; }
    public ICollection<Device> Devices { get; set; } 

    public PersonalInfo? PersonalInfo { get; set; }
}
