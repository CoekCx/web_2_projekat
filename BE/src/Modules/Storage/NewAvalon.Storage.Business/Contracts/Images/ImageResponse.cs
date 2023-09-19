using NewAvalon.Messaging.Contracts.Images;
using System;

namespace NewAvalon.Storage.Business.Contracts.Images
{
    internal sealed class ImageResponse : IImageResponse
    {
        public bool Exists { get; set; }

        public Guid ImageId { get; set; }

        public string ImageUrl { get; set; }
    }
}
