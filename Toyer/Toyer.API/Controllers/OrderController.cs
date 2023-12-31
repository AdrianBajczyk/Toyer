using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Dtos.Order;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class OrderController : ControllerBase
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IOrderMapper _mappings;

    public OrderController(IOrdersRepository ordersRepository, IOrderMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _mappings = mapper;
    }

    /// <summary>
    /// Create new order to control device.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(OrderPresentDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateOrderAsync([FromForm] OrderCreateDto orderPresentDto)
    {
        var createdOrder = await _ordersRepository.CreateNewOrderAsync(_mappings.OrderCreateDtoToOrder(orderPresentDto));

        return CreatedAtAction(nameof(CreateOrderAsync), _mappings.OrderToOrderPresentDto(createdOrder));
    }

}
