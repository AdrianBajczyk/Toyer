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

    /// <summary>
    /// Gets all orders for all devices
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderPresentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status202Accepted)]
    public async Task<IActionResult> GetAllOrdersAsync()
    {
        var allOrders = await _ordersRepository.GetAllOrdersAsync();

        return allOrders is null
            ? Accepted(new CustomResponse() { Message = "There are no orders in database yet.", StatusCode = 202 })
            : Ok(_mappings.OrdersToOrderPresentDtos(allOrders));
    }

    /// <summary>
    /// Gets order by id.
    /// </summary>
    [HttpGet("{orderId:int}")]
    [ProducesResponseType(typeof(OrderPresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderByIdAsync([FromRoute]int orderId)
    {
        var order = await _ordersRepository.GetOrderByIdAsync(orderId);

        return order is null
            ? NotFound(new CustomResponse() { Message = "Order not found", StatusCode = 404})
            : Ok(_mappings.OrderToOrderPresentDto(order));
    }

    /// <summary>
    /// Updates order credits by id.
    /// </summary>
    [HttpPut("{orderId:int}")]
    [ProducesResponseType(typeof(OrderPresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrderByIdAsync([FromRoute] int orderId, [FromForm] OrderCreateDto orderUpdates)
    {
        var order = await _ordersRepository.UpdateOrderByIdAsync(orderId, orderUpdates);

        return order is null
            ? NotFound(new CustomResponse() { Message = "Order not found", StatusCode = 404 })
            : Ok(_mappings.OrderToOrderPresentDto(order));
    }

    /// <summary>
    /// Assigns order selected by id to device types listed by id.
    /// </summary>
    [HttpPut("assignment/{orderId:int}")]
    [ProducesResponseType(typeof(OrderPresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignToDeviceTypes([FromRoute] int orderId, [FromForm] OrderAssignDto deviceTypeListIds)
    {
        var result = await _ordersRepository.AssignOrderToDeviceTypesAsync(orderId, deviceTypeListIds);

        return !result.IsValid 
            ? BadRequest(new CustomResponse { Message = result.ClientErrorMessage, StatusCode = 400 })
            : Ok(_mappings.OrderToOrderPresentDto(result.Entity!));
    }

    /// <summary>
    /// Deletes order selected by id.
    /// </summary>
    [HttpDelete("{orderId:int}")]
    [ProducesResponseType(typeof(OrderPresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrderByIdAsync([FromRoute] int orderId)
    {
        var deletedOrder = await _ordersRepository.DeleteOrderByIdAsync(orderId);

        return deletedOrder is null
            ? NotFound(new CustomResponse() { Message = "Order not found", StatusCode = 404 })
            : Ok(_mappings.OrderToOrderPresentDto(deletedOrder));
    }
}
