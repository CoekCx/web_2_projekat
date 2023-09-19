using NewAvalon.Abstractions.Clock;
using NewAvalon.Order.Domain.Repositories;
using Quartz;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewAvalon.Order.Persistence.BackgroundTasks
{
    [DisallowConcurrentExecution]
    public sealed class OrderDeliveryJob : IJob
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderUnitOfWork _unitOfWork;
        private readonly ISystemTime _systemTime;

        public OrderDeliveryJob(IOrderRepository orderRepository, IOrderUnitOfWork unitOfWork, ISystemTime systemTime)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _systemTime = systemTime;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<Domain.Entities.Order> orders = await _orderRepository.GetShippingOrdersAsync(_systemTime.UtcNow, context.CancellationToken);

            if (!orders.Any())
            {
                return;
            }

            foreach (Domain.Entities.Order order in orders)
            {
                order.Deliver();
            }

            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }
}
