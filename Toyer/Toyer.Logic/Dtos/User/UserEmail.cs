using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.User;

public record UserEmail
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

}