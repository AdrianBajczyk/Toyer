namespace Toyer.Logic.Dtos.User;

public class UserCreateDto
{
    public string Login {  get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string? PhoneNumber { get; set; }
}
