using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.Order;

public record OrderCreateDto
{
    [Required(ErrorMessage = "The name is required.")]
    [StringLength(50, ErrorMessage = "Login cannot exceed 50 characters.")]
    [RegularExpression("^[A-Z][a-z]*$", ErrorMessage = "First letter has to be uppercase, the rest have to be lower case.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The description is required.")]
    [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "The description is required.")]
    [StringLength(200, ErrorMessage = "Message body cannot exceed 200 characters.")]
    [Display(Name = "Message body")]
    public string MessageBody { get; set; }
}
