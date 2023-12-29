using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.Device;

public record DeviceNameUpdateDto
{
    [Required(ErrorMessage = "The name is required.")]
    [StringLength(20, ErrorMessage = "The name cannot exceed 20 characters.")]
    [RegularExpression("^[A-Z][a-z]*[0-9]*$", ErrorMessage = "First letter has to be uppercase, the rest have to be lowercase, and numbers are allowed at the end.")]
    public string Name { get; set; }
}
