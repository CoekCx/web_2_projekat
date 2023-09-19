using Microsoft.EntityFrameworkCore;
using NewAvalon.Order.Domain.Repositories;
using NewAvalon.Persistence.Relational;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Persistence
{
    public sealed class OrderDbContext : BaseDbContext, IOrderUnitOfWork
    {
        public OrderDbContext(DbContextOptions options)
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

            SeedOrderData(modelBuilder);
        }

        private void SeedOrderData(ModelBuilder modelBuilder)
        {
        }
    }
}
