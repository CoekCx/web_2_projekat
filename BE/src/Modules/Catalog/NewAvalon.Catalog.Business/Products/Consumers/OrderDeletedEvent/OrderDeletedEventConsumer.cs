using MassTransit;
using NewAvalon.Catalog.Domain.Entities;
using NewAvalon.Catalog.Domain.EntityIdentifiers;
using NewAvalon.Catalog.Domain.Repositories;
using NewAvalon.Messaging.Contracts.Orders;
using System.Linq;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business.Products.Consumers.OrderDeletedEvent
{
    public sealed class OrderDeletedEventConsumer : IConsumer<IOrderDeletedEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICatalogUnitOfWork _unitOfWork;

        public OrderDeletedEventConsumer(IProductRepository userRepository, ICatalogUnitOfWork unitOfWork)
        {
            _productRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<IOrderDeletedEvent> context)
        {
            var productIds = context.Message.Products.Select(product => new ProductId(product.CatalogProductId)).ToArray();

            Product[] products = await _productRepository.GetByIdsAsync(productIds, context.CancellationToken);

            if (products.Length != productIds.Length)
            {
                return;
            }

            foreach (var requestedProduct in context.Message.Products)
            {
                var catalogProduct = products.First(x => x.Id.Value == requestedProduct.CatalogProductId);

                catalogProduct.IncreaseCapacity(requestedProduct.Quantity);
            }

            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }
}
