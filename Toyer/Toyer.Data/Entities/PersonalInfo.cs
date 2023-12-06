namespace Toyer.Data.Entities;

public class PersonalInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Guid AddressId { get; set; }
    public string email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }

}
