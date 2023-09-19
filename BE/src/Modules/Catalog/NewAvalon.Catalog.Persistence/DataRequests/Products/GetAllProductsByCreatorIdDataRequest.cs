using Microsoft.EntityFrameworkCore;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProduct;
using NewAvalon.Catalog.Business.Products.Queries.GetProductsByCreator;
using NewAvalon.Catalog.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Persistence.DataRequests.Products
{
    internal sealed class GetAllProductsByCreatorIdDataRequest : IGetAllProductsByCreatorIdDataRequest
    {
        private readonly CatalogDbContext _dbContext;

        public GetAllProductsByCreatorIdDataRequest(CatalogDbContext dbContext) => _dbContext = dbContext;

        public async Task<PagedList<CatalogProductDetailsResponse>> GetAsync((Guid CreatorId, int Page, int ItemsPerPage) request, CancellationToken cancellationToken = default)
        {
            var products = await _dbContext.Set<Product>()
                .Where(product => product.CreatorId == request.CreatorId)
                .OrderBy(product => product.CreatedOnUtc)
                .Skip((request.Page - 1) * request.ItemsPerPage)
                .Take(request.ItemsPerPage)
                .ToListAsync(cancellationToken);

            var response = products
                .Select(product => new CatalogProductDetailsResponse(
                    product.Id.Value,
                    product.Name,
                    product.Price,
                    product.Capacity,
                    product.Description,
                    new ProductImageResponse(product.ProductImage.Id, product.ProductImage.Url)));

            var count = await _dbContext.Set<Product>().CountAsync(cancellationToken: cancellationToken);

            return new PagedList<CatalogProductDetailsResponse>(response, count, request.Page, request.ItemsPerPage);
        }
    }
}
