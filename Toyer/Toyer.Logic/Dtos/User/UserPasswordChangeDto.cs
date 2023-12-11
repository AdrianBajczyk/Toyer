using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.User
{
    public class UserPasswordChangeDto
    {
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [MaxLength(20, ErrorMessage = "Password cannot exceed 20 characters")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{6,20}$", ErrorMessage = "Password must have at least one uppercase letter and one digit")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
