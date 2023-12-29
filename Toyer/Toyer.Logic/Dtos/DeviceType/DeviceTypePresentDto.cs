using Toyer.Logic.Dtos.Order;

namespace Toyer.Logic.Dtos.DeviceType;

public record DeviceTypePresentDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<OrderPresentDto> Orders {  get; set; } 
}
