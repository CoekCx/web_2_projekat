using NewAvalon.Abstractions.Messaging;
using NewAvalon.Storage.Boundary.Images.Commands.UploadImage;
using NewAvalon.Storage.Business.Abstractions;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Business.Images.Commands.UploadImage
{
    internal sealed class UploadImageCommandHandler : ICommandHandler<UploadImageCommand, UploadImageResponse>
    {
        private readonly IImageStorageService _imageStorageService;
        private readonly IImageUrlFormatter _imageUrlFormatter;
        private readonly IImageRepository _imageRepository;
        private readonly IStorageUnitOfWork _unitOfWork;

        public UploadImageCommandHandler(
            IImageStorageService imageStorageService,
            IImageUrlFormatter imageUrlFormatter,
            IImageRepository imageRepository,
            IStorageUnitOfWork unitOfWork)
        {
            _imageStorageService = imageStorageService;
            _imageUrlFormatter = imageUrlFormatter;
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UploadImageResponse> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            Guid imageId = await _imageStorageService.UploadAsync(request.Image, cancellationToken);

            Uri imageUri = _imageUrlFormatter.Format(imageId);

            var image = new Image(new ImageId(imageId), imageUri.ToString());

            _imageRepository.Insert(image);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UploadImageResponse(image.Id.Value, image.Url);
        }
    }
}
