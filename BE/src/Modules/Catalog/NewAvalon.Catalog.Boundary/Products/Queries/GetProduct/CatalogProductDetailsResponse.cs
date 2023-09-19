using System;

namespace NewAvalon.Catalog.Boundary.Products.Queries.GetProduct
{
    public sealed record CatalogProductDetailsResponse(
        Guid Id,
        string Name,
        decimal Price,
        decimal Capacity,
        string Description,
        ProductImageResponse ProductImage);

    public sealed record ProductImageResponse(
        Guid Id,
        string Url);
}