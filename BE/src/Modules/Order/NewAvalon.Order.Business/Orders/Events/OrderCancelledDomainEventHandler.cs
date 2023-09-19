using NewAvalon.Abstractions.Messaging;
using NewAvalon.Domain.Abstractions;
using NewAvalon.Messaging.Contracts.Orders;
using NewAvalon.Order.Business.Contracts.Products;
using NewAvalon.Order.Domain.EntityIdentifiers;
using NewAvalon.Order.Domain.Events;
using NewAvalon.Order.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Business.Orders.Events
{
    public class OrderCancelledDomainEventHandler : IDomainEventHandler<OrderCancelledDomainEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventPublisher _publisher;

        public OrderCancelledDomainEventHandler(
            IOrderRepository orderRepository,
            IEventPublisher publisher)
        {
            _orderRepository = orderRepository;
            _publisher = publisher;
        }

        public async Task Handle(OrderCancelledDomainEvent notification, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(new OrderId(notification.OrderId), cancellationToken);

            if (order is null)
            {
                return;
            }

            var @event = new OrderDeletedEvent()
            {
                Products = order.Products.Select(product => new DeletedProductDetails
                {
                    CatalogProductId = product.CatalogProductId,
                    Quantity = product.Quantity
                }).ToArray()
            };

            await _publisher.PublishAsync<IOrderDeletedEvent>(@event, cancellationToken);
        }
    }
}
