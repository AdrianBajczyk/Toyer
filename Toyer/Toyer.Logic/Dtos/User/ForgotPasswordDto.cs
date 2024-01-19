using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.User;

public record ForgotPasswordDto
{

    [Required]
    [EmailAddress]
    public string Email { get; set; }

}
