using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.DeviceType;

public record DeviceTypeCreateDto
{
    [Required(ErrorMessage = "The name is required.")]
    [StringLength(20, ErrorMessage = "Login cannot exceed 20 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The description is required.")]
    [StringLength(60, ErrorMessage = "Description cannot exceed 60 characters.")]
    public string Description { get; set; }
}
