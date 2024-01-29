using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.Token
{
    public class TokenDto
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}