namespace Toyer.Data.Entities;

public class Order
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string MessageBody { get; set; }
    public List<DeviceType> DeviceTypes { get; set; } = new();
}
