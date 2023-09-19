using System;

namespace NewAvalon.Catalog.Boundary.Products.Commands.DeleteProduct
{
    public sealed record DeleteProductRequest(
        Guid ProductId,
        Guid CreatorId);
}
