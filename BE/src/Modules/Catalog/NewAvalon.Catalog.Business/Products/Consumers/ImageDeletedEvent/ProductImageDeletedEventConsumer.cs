using MassTransit;
using NewAvalon.Catalog.Domain.Entities;
using NewAvalon.Catalog.Domain.Repositories;
using NewAvalon.Messaging.Contracts.Images;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business.Products.Consumers.ImageDeletedEvent
{
    public sealed class ProductImageDeletedEventConsumer : IConsumer<IImageDeletedEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICatalogUnitOfWork _unitOfWork;

        public ProductImageDeletedEventConsumer(IProductRepository userRepository, ICatalogUnitOfWork unitOfWork)
        {
            _productRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<IImageDeletedEvent> context)
        {
            Product product = await _productRepository.GetByImageIdAsync(context.Message.ImageId, context.CancellationToken);

            if (product is null)
            {
                return;
            }

            product.RemoveProductImage();

            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }
}
