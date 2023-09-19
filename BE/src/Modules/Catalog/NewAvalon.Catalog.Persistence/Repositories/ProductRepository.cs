using Microsoft.EntityFrameworkCore;
using NewAvalon.Catalog.Domain.Entities;
using NewAvalon.Catalog.Domain.EntityIdentifiers;
using NewAvalon.Catalog.Domain.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Persistence.Repositories
{
    internal sealed class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _dbContext;

        public ProductRepository(CatalogDbContext dbContext) => _dbContext = dbContext;

        public async Task<Product> GetByIdAsync(ProductId productId, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<Product>()
                .FirstOrDefaultAsync(product => product.Id == productId, cancellationToken);

        public async Task<Product[]> GetByIdsAsync(ProductId[] productIds, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<Product>()
                .Where(x => productIds.Contains(x.Id)).ToArrayAsync(cancellationToken);

        public async Task<bool> ExistsAsync(ProductId productId, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<Product>().AnyAsync(product => product.Id == productId, cancellationToken);

        public async Task<Product> GetByImageIdAsync(Guid imageId, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<Product>().FirstOrDefaultAsync(user => user.ProductImage.Id == imageId, cancellationToken);

        public void Insert(Product product) => _dbContext.Set<Product>().Add(product);

        public void Delete(Product product) => _dbContext.Set<Product>().Remove(product);
    }
}
