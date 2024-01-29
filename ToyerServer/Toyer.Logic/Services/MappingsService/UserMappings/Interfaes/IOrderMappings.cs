using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Order;

namespace Toyer.Logic.Services.Mappings.UserMappings.Interfaes
{
    public interface IOrderMappings
    {
        Order OrderCreateDtoToOrder(OrderCreateDto orderCreateDto);
        OrderPresentDto OrderToOrderPresentDto(Order createdOrder);
        IEnumerable<OrderPresentDto> OrdersToOrderPresentDtos(IEnumerable<Order> orders);
    }
}