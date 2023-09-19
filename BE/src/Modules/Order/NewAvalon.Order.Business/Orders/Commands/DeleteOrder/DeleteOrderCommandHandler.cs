using MediatR;
using NewAvalon.Abstractions.Clock;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Order.Boundary.Orders.Commands.DeleteOrder;
using NewAvalon.Order.Domain.EntityIdentifiers;
using NewAvalon.Order.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Business.Orders.Commands.DeleteOrder
{
    internal sealed class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderUnitOfWork _unitOfWork;
        private readonly ISystemTime _systemTime;

        public DeleteOrderCommandHandler(
            IOrderRepository orderRepository,
            IOrderUnitOfWork unitOfWork,
            ISystemTime systemTime)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _systemTime = systemTime;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(new OrderId(request.OrderId), cancellationToken);

            if ((_systemTime.UtcNow - order.CreatedOnUtc).TotalSeconds < 3600)
            {
                order.Cancel();
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
