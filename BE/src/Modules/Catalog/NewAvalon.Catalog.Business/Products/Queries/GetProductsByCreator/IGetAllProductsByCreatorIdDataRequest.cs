using NewAvalon.Abstractions.Data;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProduct;
using System;

namespace NewAvalon.Catalog.Business.Products.Queries.GetProductsByCreator
{
    public interface IGetAllProductsByCreatorIdDataRequest : IDataRequest<(Guid CreatorId, int Page, int ItemsPerPage), PagedList<CatalogProductDetailsResponse>>
    {
    }
}
