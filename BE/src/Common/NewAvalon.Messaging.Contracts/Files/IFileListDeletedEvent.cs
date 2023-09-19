using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Messaging.Contracts.Files
{
    public interface IFileListDeletedEvent : IEvent
    {
        Guid[] FileIds { get; set; }
    }
}
