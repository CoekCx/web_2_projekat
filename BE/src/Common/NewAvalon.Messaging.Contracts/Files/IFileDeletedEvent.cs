using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Messaging.Contracts.Files
{
    public interface IFileDeletedEvent : IEvent
    {
        Guid FileId { get; set; }
    }
}
