
using System.ComponentModel.DataAnnotations;

namespace Toyer.Data.Entities;

public class Device
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTime? LastRegistrationDate { get; set; }
    public string StaSsid { get; set; }
    public string StaPass { get; set; }
    public string? ApSsid { get; set; }
    public string? ApPass { get; set; }


    public Guid UserId { get; set; }
    public User User { get; set; }
    public int DeviceTypeId { get; set; }
    public DeviceType? DeviceType { get; set; }

}
