using System.ComponentModel.DataAnnotations;

namespace Toyer.API.Controllers;

public record EmailMessageDataDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string ContactEmail { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 20, ErrorMessage = "Message length must be between 20 and 500 characters.")]
    public string Message { get; set; }
}