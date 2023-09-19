using Microsoft.AspNetCore.Http;
using NewAvalon.Abstractions.Messaging;

namespace NewAvalon.Storage.Boundary.Files.Commands.UploadFile
{
    public sealed record UploadFileCommand(IFormFile File) : ICommand<UploadFileResponse>;
}
