using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;

namespace Toyer.Data.Entities;

public class PersonalInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
    public Address? Address { get; set; }
}
