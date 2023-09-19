using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Messaging.Contracts.Images
{
    public interface IImageDeletedEvent : IEvent
    {
        Guid ImageId { get; set; }
    }
}
