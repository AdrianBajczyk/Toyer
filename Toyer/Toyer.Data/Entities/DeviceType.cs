namespace Toyer.Data.Entities;

public class DeviceType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Device> Devices { get; set; } = new HashSet<Device>();
    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}
