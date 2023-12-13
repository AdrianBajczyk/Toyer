using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.User;

public class UserPersonalInfoDto
{
    [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters")]
    public string? Name { get; set; }

    [StringLength(20, ErrorMessage = "Surname cannot exceed 20 characters")]
    public string? Surname { get; set; }

    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    // https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-8.0
    public string? Email { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateOnly? BirthDate { get; set; }

    [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$", ErrorMessage = "Invalid phone number format")]
    public string? PhoneNumber { get; set; }
}
