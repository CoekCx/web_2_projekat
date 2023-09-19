using NewAvalon.Messaging.Contracts.Images;
using System;

namespace NewAvalon.Storage.Business.Contracts.Images
{
    internal sealed class ImageDeletedEvent : IImageDeletedEvent
    {
        public Guid ImageId { get; set; }
    }
}
