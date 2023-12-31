using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.Order;

public record OrderCreateDto
{
    [Required(ErrorMessage = "The name is required.")]
    [StringLength(20, ErrorMessage = "Login cannot exceed 20 characters.")]
    [RegularExpression("^[A-Z][a-z]*$", ErrorMessage = "First letter has to be uppercase, the rest have to be lower case.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The description is required.")]
    [StringLength(60, ErrorMessage = "Description cannot exceed 60 characters.")]
    [RegularExpression(@"(?:^[A-Z]|[.!?]\s+[A-Z])", ErrorMessage = "Check if every beginning of sentence is followed by capital letter.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "The description is required.")]
    [StringLength(60, ErrorMessage = "Message body cannot exceed 50 characters.")]
    [Display(Name = "Message body")]
    public string MessageBody { get; set; }
}
