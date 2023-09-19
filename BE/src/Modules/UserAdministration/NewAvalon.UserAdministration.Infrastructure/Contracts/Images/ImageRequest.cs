using NewAvalon.Messaging.Contracts.Images;
using System;

namespace NewAvalon.UserAdministration.Infrastructure.Contracts.Images
{
    internal sealed class ImageRequest : IImageRequest
    {
        public Guid ImageId { get; set; }
    }
}
