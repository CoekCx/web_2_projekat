using NewAvalon.Abstractions.Messaging;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProduct;
using System;

namespace NewAvalon.Catalog.Boundary.Products.Queries.GetProductsByCreator
{
    public sealed record GetProductsByCreatorQuery(Guid CreatorId, int Page, int ItemsPerPage) : IQuery<PagedList<CatalogProductDetailsResponse>>;
}
