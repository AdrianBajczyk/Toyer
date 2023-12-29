using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.Order;

public record OrderPresentDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}
