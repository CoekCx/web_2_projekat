using NewAvalon.Domain.Errors;
using System;

namespace NewAvalon.Domain.Exceptions.Images
{
    public sealed class ImageNotFoundException : NotFoundException
    {
        public ImageNotFoundException(Guid imageId)
            : base(
                "Image not found",
                $"The image with the identifier {imageId} was not found.",
                Error.ImageNotFound)
        {
        }
    }
}
