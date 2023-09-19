using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Storage.Boundary.Files.Queries.DownloadFile
{
    public sealed record DownloadFileQuery(Guid FileId) : IQuery<DownloadFileResponse>;
}
