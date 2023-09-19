using NewAvalon.Abstractions.Messaging;
using NewAvalon.Catalog.Business.Contracts.Products;
using NewAvalon.Catalog.Domain.Events;
using NewAvalon.Domain.Abstractions;
using NewAvalon.Messaging.Contracts.Products;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business.Products.Events.ProductImageChanged
{
    internal sealed class ProductImageChangedDomainEventHandler : IDomainEventHandler<ProductImageChangedDomainEvent>
    {
        private readonly IEventPublisher _eventPublisher;

        public ProductImageChangedDomainEventHandler(IEventPublisher eventPublisher) => _eventPublisher = eventPublisher;

        public async Task Handle(ProductImageChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var @event = new ProductImageChangedEvent
            {
                OldImageId = notification.OldImageId
            };

            await _eventPublisher.PublishAsync<IProductImageChangedEvent>(@event, cancellationToken);
        }
    }
}