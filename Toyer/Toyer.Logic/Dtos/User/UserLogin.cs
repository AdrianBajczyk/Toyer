using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.User;

public record UserLogin
{
    [Required]
    [DataType(DataType.EmailAddress)] 
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

}
