using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.Token
{
    public class TokenDto
    {
        [Required]
        public string Token { get; set; }

    }
}