using Microsoft.AspNetCore.Http;
using NewAvalon.Abstractions.Messaging;

namespace NewAvalon.Storage.Boundary.Images.Commands.UploadImage
{
    public sealed record UploadImageCommand(IFormFile Image) : ICommand<UploadImageResponse>;
}
