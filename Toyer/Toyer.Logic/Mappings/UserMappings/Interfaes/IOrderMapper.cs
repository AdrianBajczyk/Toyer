using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Order;

namespace Toyer.API.Controllers
{
    public interface IOrderMapper
    {
        Order OrderCreateDtoToOrder(OrderCreateDto orderPresentDto);
        OrderPresentDto OrderToOrderPresentDto(Order createdOrder);
    }
}