using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Dtos.Order;
using Toyer.Logic.Exceptions;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _ordersRepository;
    private readonly IOrderMappings _mappings;

    public OrderController(IOrderRepository ordersRepository, IOrderMappings mapper)
    {
        _ordersRepository = ordersRepository;
        _mappings = mapper;
    }

    /// <summary>
    /// Create new order to control device.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(OrderPresentDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateOrderAsync([FromForm] OrderCreateDto orderCreateDto)
    {
        var createdOrder = await _ordersRepository.CreateNewOrderAsync(_mappings.OrderCreateDtoToOrder(orderCreateDto));

        return CreatedAtAction(nameof(CreateOrderAsync), _mappings.OrderToOrderPresentDto(createdOrder));
    }

    ///<summary>
    /// Gets all orders for all devices
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(OrderPresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllOrdersAsync()
    {
        var allOrders = await _ordersRepository.GetAllOrdersAsync();

        return allOrders is null
            ? Accepted(new CustomResponse() { Message = "There are no orders in database yet.", StatusCode = 202 })
            : Ok(_mappings.OrdersToOrderPresentDtos(allOrders));
    }

}
