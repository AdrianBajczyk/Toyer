using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.Order;

public record OrderAssignDto
{
    [Required(ErrorMessage = "The device type/types are required.")]
    [Display(Name = "Device Type IDs")]
    public List<int> DeviceTypeId { get; set; }
}
