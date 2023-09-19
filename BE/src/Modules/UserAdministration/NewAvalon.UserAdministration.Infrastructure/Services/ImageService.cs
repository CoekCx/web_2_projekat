using MassTransit;
using NewAvalon.Abstractions.ServiceLifetimes;
using NewAvalon.Messaging.Contracts.Images;
using NewAvalon.UserAdministration.Business.Abstractions;
using NewAvalon.UserAdministration.Infrastructure.Contracts.Images;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Infrastructure.Services
{
    internal sealed class ImageService : IImageService, ITransient
    {
        private readonly IRequestClient<IImageRequest> _imageRequestClient;

        public ImageService(IRequestClient<IImageRequest> imageRequestClient) => _imageRequestClient = imageRequestClient;

        public async Task<IImageResponse> GetByIdAsync(Guid imageId, CancellationToken cancellationToken)
        {
            var request = new ImageRequest
            {
                ImageId = imageId
            };

            Response<IImageResponse> response = await _imageRequestClient.GetResponse<IImageResponse>(request, cancellationToken);

            return response.Message;
        }
    }
}
