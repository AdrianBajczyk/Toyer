using System.ComponentModel.DataAnnotations;

namespace Toyer.Data.Entities;

public record UserDevices
{
    [Key]
    public string UserId { get; set; }
    public List<Device?> Devices { get; set; } = new();
}
