using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.User;

public record UserLogin
{
    [DataType(DataType.EmailAddress)] 
    public string Email { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }

}