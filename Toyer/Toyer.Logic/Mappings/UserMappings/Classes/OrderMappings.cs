using AutoMapper;
using Toyer.API.Controllers;
using Toyer.Data.Entities;
using Toyer.Logic.Dtos.Order;

namespace Toyer.Logic.Mappings.UserMappings.Classes;

public class OrderMappings : IOrderMappings
{
    private readonly IMapper _mapper;

    public OrderMappings(IMapper mapper) => _mapper = mapper;
    public Order OrderCreateDtoToOrder(OrderCreateDto orderCreateDto) => _mapper.Map<Order>(orderCreateDto);
    public IEnumerable<OrderPresentDto> OrdersToOrderPresentDtos(IEnumerable<Order> orders) => _mapper.Map<IEnumerable<OrderPresentDto>>(orders);
    public OrderPresentDto OrderToOrderPresentDto(Order createdOrder) => _mapper.Map<OrderPresentDto>(createdOrder);
}
