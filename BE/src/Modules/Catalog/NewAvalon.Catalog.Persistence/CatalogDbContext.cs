using Microsoft.EntityFrameworkCore;
using NewAvalon.Catalog.Domain.Repositories;
using NewAvalon.Persistence.Relational;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Persistence
{
    public sealed class CatalogDbContext : BaseDbContext, ICatalogUnitOfWork
    {
        public CatalogDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) =>
            await Database.BeginTransactionAsync(cancellationToken);

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default) =>
            await Database.CommitTransactionAsync(cancellationToken);

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) =>
            await Database.RollbackTransactionAsync(cancellationToken);

        protected override void OnModelCreatingInternal(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            AddOutbox(modelBuilder);

            SeedCatalogData(modelBuilder);
        }

        private void SeedCatalogData(ModelBuilder modelBuilder)
        {
        }
    }
}
