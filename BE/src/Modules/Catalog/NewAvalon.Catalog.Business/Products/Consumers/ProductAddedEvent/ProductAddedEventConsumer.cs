using MassTransit;
using NewAvalon.Catalog.Domain.Entities;
using NewAvalon.Catalog.Domain.EntityIdentifiers;
using NewAvalon.Catalog.Domain.Repositories;
using NewAvalon.Messaging.Contracts.Products;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business.Products.Consumers.ProductAddedEvent
{
    public sealed class ProductAddedEventConsumer : IConsumer<IProductAddedEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICatalogUnitOfWork _unitOfWork;

        public ProductAddedEventConsumer(IProductRepository userRepository, ICatalogUnitOfWork unitOfWork)
        {
            _productRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<IProductAddedEvent> context)
        {
            Product product = await _productRepository.GetByIdAsync(new ProductId(context.Message.ProductId), context.CancellationToken);

            if (product is null)
            {
                return;
            }

            product.ProductAdded(context.Message.Quantity);

            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }
}
