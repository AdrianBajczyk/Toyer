namespace Toyer.Logic.Dtos.User;

public class UserPersonalInfoDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Guid AddressId { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }
}
