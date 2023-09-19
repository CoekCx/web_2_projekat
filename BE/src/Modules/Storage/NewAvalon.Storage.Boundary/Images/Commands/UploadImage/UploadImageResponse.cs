using System;

namespace NewAvalon.Storage.Boundary.Images.Commands.UploadImage
{
    public record UploadImageResponse(Guid Id, string Url);
}
