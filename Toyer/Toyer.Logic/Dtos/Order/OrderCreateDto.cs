using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.Order;

public record OrderDto
{
    [Required(ErrorMessage = "The id of order is required.")]
    public int Id { get; set; }
}
