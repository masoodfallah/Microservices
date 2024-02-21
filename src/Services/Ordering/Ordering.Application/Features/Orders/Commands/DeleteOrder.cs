using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Commands;

public static class DeleteOrder
{
    public record Command : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<Handler> _logger;


        public Handler(IOrderRepository orderRepository, IMapper mapper, ILogger<Handler> logger) =>
        (_orderRepository, _mapper, _logger) =
           (orderRepository ?? throw new ArgumentNullException(nameof(orderRepository)),
            mapper ?? throw new ArgumentNullException(nameof(mapper)),
            logger ?? throw new ArgumentNullException(nameof(logger)));

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            await _orderRepository.DeleteAsync(request.Id);

            _logger.LogInformation($"Order {request.Id} is successfully deleted");

            return Unit.Value;
        }
    }
}
