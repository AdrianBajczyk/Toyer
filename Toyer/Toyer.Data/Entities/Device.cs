using Microsoft.OpenApi.Any;

namespace Toyer.Data.Entities;

public class Device
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? LastRegistrationDate { get; set; }
    public int TypeId { get; set; }
    public Guid? UserId { get; set; }
    public string? StaSsid { get; set; }
    public BinaryData? StaPass { get; set; }
    public string? ApSsid { get; set; }
    public BinaryData? ApPass { get; set; }

}
