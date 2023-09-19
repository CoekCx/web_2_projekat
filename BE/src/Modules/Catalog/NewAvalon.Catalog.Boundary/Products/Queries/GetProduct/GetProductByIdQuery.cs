using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Catalog.Boundary.Products.Queries.GetProduct
{
    public sealed record GetProductByIdQuery(Guid ProductId) : IQuery<CatalogProductDetailsResponse>;
}
