using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.User
{
    public record UserPasswordChangeDto
    {

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
