using System.ComponentModel.DataAnnotations;
using Toyer.Logic.Dtos.DeviceType;

namespace Toyer.Logic.Dtos.Device;

public record DeviceCreateDto
{
    [Required(ErrorMessage = "The name is required.")]
    [StringLength(20, ErrorMessage = "The name cannot exceed 20 characters.")]
    [RegularExpression("^[A-Z][a-z]*[0-9]*$", ErrorMessage = "First letter has to be uppercase, the rest have to be lowercase, and numbers are allowed at the end.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The device type id is required.")]
    [Display(Name = "Device type id")]
    public int DeviceTypeId { get; set; }

}
