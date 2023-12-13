using System.Text.Json.Serialization;

namespace Toyer.Data.Entities;

public class PersonalInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }

    public Guid UserId { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    public Address? Address { get; set; }
}
