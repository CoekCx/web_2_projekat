using System;

namespace NewAvalon.Catalog.Boundary.Products.Commands.UpdateProductImage
{
    public sealed record UpdateProductImageRequest(Guid ProductId, Guid? ImageId);
}
