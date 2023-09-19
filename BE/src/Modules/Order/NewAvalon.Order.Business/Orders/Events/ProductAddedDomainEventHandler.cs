using NewAvalon.Abstractions.Messaging;
using NewAvalon.Domain.Abstractions;
using NewAvalon.Messaging.Contracts.Products;
using NewAvalon.Order.Business.Contracts.Products;
using NewAvalon.Order.Domain.Events;
using NewAvalon.Order.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Business.Orders.Events
{
    public class ProductAddedDomainEventHandler : IDomainEventHandler<ProductAddedDomainEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IEventPublisher _publisher;

        public ProductAddedDomainEventHandler(
            IProductRepository productRepository,
            IEventPublisher publisher)
        {
            _productRepository = productRepository;
            _publisher = publisher;
        }

        public async Task Handle(ProductAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(notification.ProductId, cancellationToken);

            if (product is null)
            {
                return;
            }

            var @event = new ProductAddedEvent()
            {
                ProductId = product.CatalogProductId,
                Quantity = product.Quantity
            };

            await _publisher.PublishAsync<IProductAddedEvent>(@event, cancellationToken);
        }
    }
}
