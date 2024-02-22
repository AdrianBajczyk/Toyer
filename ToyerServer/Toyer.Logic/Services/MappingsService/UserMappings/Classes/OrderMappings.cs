using AutoMapper;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Order;
using Toyer.Logic.Services.Mappings.UserMappings.Interfaes;

namespace Toyer.Logic.Services.Mappings.UserMappings.Classes;

public class OrderMappings(IMapper mapper) : IOrderMappings
{
    private readonly IMapper _mapper = mapper;

    public Order OrderCreateDtoToOrder(OrderCreateDto orderCreateDto) => _mapper.Map<Order>(orderCreateDto);
    public IEnumerable<OrderPresentDto> OrdersToOrderPresentDtos(IEnumerable<Order> orders) => _mapper.Map<IEnumerable<OrderPresentDto>>(orders);
    public OrderPresentDto OrderToOrderPresentDto(Order createdOrder) => _mapper.Map<OrderPresentDto>(createdOrder);
}
