using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.DeviceType;

public record DeviceTypeCreateDto
{
    [Required(ErrorMessage = "The name is required.")]
    [StringLength(30, ErrorMessage = "Login cannot exceed 30 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The description is required.")]
    [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
    public string Description { get; set; }

    [StringLength(200, ErrorMessage = "URL cannot exceed 200 characters.")]
    public string? ImageUrl { get; set; }
}
