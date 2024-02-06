using AutoMapper;
using Ordering.Application.Features.Orders.Queries;
using Ordering.Domain.Entities;
using static Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using static Ordering.Application.Features.Orders.Commands.UpdateOrder;

namespace Ordering.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrdersVm>().ReverseMap();
        CreateMap<Order, CheckoutOrderRequest>().ReverseMap();
        CreateMap<Order, UpdateOrderRequest>().ReverseMap();
    }
}
