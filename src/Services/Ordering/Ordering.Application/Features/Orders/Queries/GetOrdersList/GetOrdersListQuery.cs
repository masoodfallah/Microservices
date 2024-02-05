using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQuery : IRequest<List<OrdersVm>>
{
    public string UserName { get; set; }

    public GetOrdersListQuery(string UserName)
    {
        UserName = UserName ?? throw new ArgumentNullException(nameof(UserName));
    }
}


public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        var orderList = await _orderRepository.GetOrderByUserName(request.UserName);
        return _mapper.Map<List<OrdersVm>>(orderList);
    }
}


public class OrdersVm
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public decimal TotalPrice { get; set; }

    //Billing Address
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    //Payment
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string Expiration { get; set; }
    public string CVV { get; set; }
    public int PaymentMethod { get; set; }
}