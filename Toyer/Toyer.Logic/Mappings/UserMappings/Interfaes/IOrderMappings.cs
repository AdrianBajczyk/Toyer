using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Order;

namespace Toyer.API.Controllers
{
    public interface IOrderMappings
    {
        Order OrderCreateDtoToOrder(OrderCreateDto orderCreateDto);
        OrderPresentDto OrderToOrderPresentDto(Order createdOrder);
    }
}