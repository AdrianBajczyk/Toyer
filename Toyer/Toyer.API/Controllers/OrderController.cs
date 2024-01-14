using Azure;
using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Dtos.Order;
using Toyer.Logic.Responses;

namespace Toyer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class OrderController(IOrderRepository ordersRepository, IOrderMappings mapper) : ControllerBase
{
    private readonly IOrderRepository _ordersRepository = ordersRepository;
    private readonly IOrderMappings _mappings = mapper;

    /// <summary>
    /// Create new order to control device.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(OrderPresentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllOrdersAsync()
    {
        var allOrders = await _ordersRepository.GetAllOrdersAsync();

        return Ok(_mappings.OrdersToOrderPresentDtos(allOrders));
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

        return Ok(_mappings.OrderToOrderPresentDto(order));
    }

    /// <summary>
    /// Updates order credits by id.
    /// </summary>
    [HttpPut("{orderId:int}")]
    [ProducesResponseType(typeof(OrderPresentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateOrderByIdAsync([FromRoute] int orderId, [FromForm] OrderCreateDto orderUpdates)
    {
        await _ordersRepository.UpdateOrderByIdAsync(orderId, orderUpdates);

        return NoContent();
    }

    /// <summary>
    /// Assigns order to device types.
    /// </summary>
    [HttpPut("assignment/{orderId:int}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignToDeviceTypesAsync([FromRoute] int orderId, [FromForm] OrderAssignDto deviceTypeList)
    {
       await _ordersRepository.AssignOrderToDeviceTypesAsync(orderId, deviceTypeList.DeviceTypeIds);

       return NoContent();
    }

    /// <summary>
    /// Deletes order selected by id.
    /// </summary>
    [HttpDelete("{orderId:int}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrderByIdAsync([FromRoute] int orderId)
    {
        await _ordersRepository.DeleteOrderByIdAsync(orderId);

        return NoContent();
    }
}
