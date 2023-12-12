namespace Toyer.Data.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public DateTime AccCreationDate { get; set; } = DateTime.Now;

    public ICollection<Device> Devices { get; set; } = new HashSet<Device>();

    public PersonalInfo? PersonalInfo { get; set; }
}
