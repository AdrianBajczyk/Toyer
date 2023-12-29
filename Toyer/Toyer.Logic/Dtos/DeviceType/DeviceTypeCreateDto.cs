using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.DeviceType;

public record DeviceTypeCreateDto
{
    [Required(ErrorMessage = "The name is required.")]
    [StringLength(20, ErrorMessage = "Login cannot exceed 20 characters.")]
    [RegularExpression("^[A-Z][a-z]*$", ErrorMessage = "First letter has to be uppercase, the rest have to be lower case.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The description is required.")]
    [StringLength(60, ErrorMessage = "Description cannot exceed 60 characters.")]
    public string Description { get; set; }
}
