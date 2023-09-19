using MassTransit;
using NewAvalon.Messaging.Contracts.Products;
using NewAvalon.Storage.Business.Abstractions;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Domain.Repositories;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Business.Images.Consumers
{
    public class ProductImageChangedEventConsumer : IConsumer<IProductImageChangedEvent>
    {
        private readonly IImageStorageService _imageStorageService;
        private readonly IImageRepository _imageRepository;
        private readonly IStorageUnitOfWork _unitOfWork;

        public ProductImageChangedEventConsumer(IImageStorageService imageStorageService, IImageRepository imageRepository, IStorageUnitOfWork unitOfWork)
        {
            _imageStorageService = imageStorageService;
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<IProductImageChangedEvent> context)
        {
            await _imageStorageService.DeleteAsync(context.Message.OldImageId, context.CancellationToken);

            Image image = await _imageRepository.GetByIdAsync(new ImageId(context.Message.OldImageId), context.CancellationToken);

            if (image is null)
            {
                return;
            }

            _imageRepository.Remove(image);

            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }
}
