using System.ComponentModel.DataAnnotations;
using Toyer.Logic.Dtos.DeviceType;

namespace Toyer.Logic.Dtos.Device;

public record DeviceCreateDto
{
    [Required(ErrorMessage = "The device type id is required.")]
    [Display(Name = "Device type id")]
    public int DeviceTypeId { get; set; }
}
