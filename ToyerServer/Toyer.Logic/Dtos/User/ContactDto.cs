using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.User;

public record ContactDto
{
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }

    [RegularExpression(@"^\(\+\d{2}\) \d{3}-\d{3}-\d{3}$", ErrorMessage = "Invalid phone number format")]
    [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
    [Display(Name = "Phone number")]
    public string? PhoneNumber { get; set; }
}
