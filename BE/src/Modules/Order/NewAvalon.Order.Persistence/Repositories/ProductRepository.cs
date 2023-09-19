using Microsoft.EntityFrameworkCore;
using NewAvalon.Order.Domain.Entities;
using NewAvalon.Order.Domain.EntityIdentifiers;
using NewAvalon.Order.Domain.Enums;
using NewAvalon.Order.Domain.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Persistence.Repositories
{
    internal sealed class ProductRepository : IProductRepository
    {
        private readonly OrderDbContext _dbContext;

        public ProductRepository(OrderDbContext dbContext) => _dbContext = dbContext;

        public async Task<Product> GetByIdAsync(ProductId productId, CancellationToken cancellationToken = default) =>
            await _dbContext.Set<Product>().FirstOrDefaultAsync(product => product.Id == productId, cancellationToken);

        public async Task<(bool IsUsed, bool IsCurrentlyInUse)> ExistsByCatalogIdAsync(Guid catalogProductId, CancellationToken cancellationToken = default)
        {
            var isUsed = await _dbContext.Set<Domain.Entities.Order>()
                .Include(order => order.Products)
                .AsNoTracking()
                .AnyAsync(order => order.Products.Any(x => x.CatalogProductId == catalogProductId), cancellationToken);

            var isCurrentlyInUse = await _dbContext.Set<Domain.Entities.Order>()
                .Include(order => order.Products)
                .AsNoTracking()
                .Where(order => order.Status == OrderStatus.Shipping)
                .AnyAsync(x => x.Products.Any(x => x.CatalogProductId == catalogProductId), cancellationToken);

            return (isUsed, isCurrentlyInUse);
        }
    }
}