using System;

namespace NewAvalon.Storage.Boundary.Files.Queries.DownloadFile
{
    public sealed record DownloadFileResponse(Guid Id, string Name, string ContentType, byte[] Content);
}
