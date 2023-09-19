using System;

namespace NewAvalon.Storage.Boundary.Files.Commands.UploadFile
{
    public record UploadFileResponse(Guid Id, string Url, string Name, string Extension, decimal Size);
}
