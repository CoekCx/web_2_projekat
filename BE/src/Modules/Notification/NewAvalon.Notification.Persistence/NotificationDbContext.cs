using Microsoft.EntityFrameworkCore;
using NewAvalon.Notification.Domain.Repositories;
using NewAvalon.Persistence.Relational;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Notification.Persistence
{
    public sealed class NotificationDbContext : BaseDbContext, INotificationUnitOfWork
    {
        public NotificationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) =>
            await Database.BeginTransactionAsync(cancellationToken);

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default) =>
            await Database.CommitTransactionAsync(cancellationToken);

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) =>
            await Database.RollbackTransactionAsync(cancellationToken);

        protected override void OnModelCreatingInternal(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
