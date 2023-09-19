using MediatR;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Domain.Exceptions.Images;
using NewAvalon.Storage.Boundary.Images.Commands.DeleteImage;
using NewAvalon.Storage.Business.Abstractions;
using NewAvalon.Storage.Business.Contracts.Images;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Business.Images.Commands.DeleteImage
{
    internal sealed class DeleteImageCommandHandler : ICommandHandler<DeleteImageCommand, Unit>
    {
        private readonly IImageStorageService _imageStorageService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IImageRepository _imageRepository;
        private readonly IStorageUnitOfWork _unitOfWork;

        public DeleteImageCommandHandler(
            IImageStorageService imageStorageService,
            IEventPublisher eventPublisher,
            IImageRepository imageRepository,
            IStorageUnitOfWork unitOfWork)
        {
            _imageStorageService = imageStorageService;
            _eventPublisher = eventPublisher;
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            Image image = await _imageRepository.GetByIdAsync(new ImageId(request.ImageId), cancellationToken);

            if (image is null)
            {
                throw new ImageNotFoundException(request.ImageId);
            }

            await _imageStorageService.DeleteAsync(image.Id.Value, cancellationToken);

            _imageRepository.Remove(image);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _eventPublisher.PublishAsync(new ImageDeletedEvent { ImageId = request.ImageId }, cancellationToken);

            return Unit.Value;
        }
    }
}
