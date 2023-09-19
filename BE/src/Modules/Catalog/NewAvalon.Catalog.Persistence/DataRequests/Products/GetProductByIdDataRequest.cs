using Microsoft.EntityFrameworkCore;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProduct;
using NewAvalon.Catalog.Business.Products.Queries.GetProduct;
using NewAvalon.Catalog.Domain.Entities;
using NewAvalon.Catalog.Domain.EntityIdentifiers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Persistence.DataRequests.Products
{
    internal sealed class GetProductByIdDataRequest : IGetProductByIdDataRequest
    {
        private readonly CatalogDbContext _dbContext;

        public GetProductByIdDataRequest(CatalogDbContext dbContext) => _dbContext = dbContext;

        public async Task<CatalogProductDetailsResponse> GetAsync(ProductId request, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<Product>()
                .Where(product => product.Id == request)
                .Select(product => new CatalogProductDetailsResponse(
                    product.Id.Value,
                    product.Name,
                    product.Price,
                    product.Capacity,
                    product.Description,
                    new ProductImageResponse(
                        product.ProductImage.Id,
                        product.ProductImage.Url)))
                .FirstOrDefaultAsync(cancellationToken);
    }
}
