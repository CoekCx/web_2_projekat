using MassTransit;
using NewAvalon.Messaging.Contracts.Images;
using NewAvalon.Storage.Business.Contracts.Images;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Business.Images.Consumers
{
    public sealed class ImageRequestConsumer : IConsumer<IImageRequest>
    {
        private readonly IImageRepository _imageRepository;

        public ImageRequestConsumer(IImageRepository imageRepository) => _imageRepository = imageRepository;

        public async Task Consume(ConsumeContext<IImageRequest> context)
        {
            Image image = await _imageRepository.GetByIdAsync(new ImageId(context.Message.ImageId), context.CancellationToken);

            var response = new ImageResponse
            {
                Exists = image is not null,
                ImageId = image?.Id?.Value ?? Guid.Empty,
                ImageUrl = image?.Url
            };

            await context.RespondAsync<IImageResponse>(response);
        }
    }
}
