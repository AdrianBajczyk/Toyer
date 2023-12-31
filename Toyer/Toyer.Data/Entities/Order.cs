namespace Toyer.Data.Entities;

public class Order
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string MessageBody { get; set; }
    public ICollection<DeviceType> DeviceTypes { get; set; } = new HashSet<DeviceType>();
}
