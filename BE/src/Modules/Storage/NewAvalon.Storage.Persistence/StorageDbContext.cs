using Microsoft.EntityFrameworkCore;
using NewAvalon.Persistence.Relational;
using NewAvalon.Storage.Domain.Repositories;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Persistence
{
    public sealed class StorageDbContext : BaseDbContext, IStorageUnitOfWork
    {
        public StorageDbContext(DbContextOptions options)
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
        }
    }
}
