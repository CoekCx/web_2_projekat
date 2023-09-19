using System;

namespace NewAvalon.Messaging.Contracts.Images
{
    public interface IImageResponse
    {
        bool Exists { get; set; }

        Guid ImageId { get; set; }

        string ImageUrl { get; set; }
    }
}
