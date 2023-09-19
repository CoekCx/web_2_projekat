using System;

namespace NewAvalon.Messaging.Contracts.Files
{
    public interface IFileListRequest
    {
        Guid[] FileIds { get; set; }
    }
}
