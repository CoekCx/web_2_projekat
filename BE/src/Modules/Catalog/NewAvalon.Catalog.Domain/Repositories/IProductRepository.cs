using NewAvalon.Catalog.Domain.Entities;
using NewAvalon.Catalog.Domain.EntityIdentifiers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(ProductId productId, CancellationToken cancellationToken = default);

        Task<Product[]> GetByIdsAsync(ProductId[] productIds, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(ProductId productId, CancellationToken cancellationToken = default);

        Task<Product> GetByImageIdAsync(Guid imageId, CancellationToken cancellationToken = default);

        void Insert(Product product);

        void Delete(Product product);
    }
}
