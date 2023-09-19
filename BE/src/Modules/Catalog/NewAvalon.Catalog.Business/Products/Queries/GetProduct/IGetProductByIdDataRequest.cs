using NewAvalon.Abstractions.Data;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProduct;
using NewAvalon.Catalog.Domain.EntityIdentifiers;

namespace NewAvalon.Catalog.Business.Products.Queries.GetProduct
{
    public interface IGetProductByIdDataRequest : IDataRequest<ProductId, CatalogProductDetailsResponse>
    {
    }
}
