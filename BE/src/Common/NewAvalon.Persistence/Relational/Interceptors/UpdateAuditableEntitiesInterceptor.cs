using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NewAvalon.Abstractions.Clock;
using NewAvalon.Domain.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Persistence.Relational.Interceptors
{
    public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
    {
        private readonly ISystemTime _systemTime;

        public UpdateAuditableEntitiesInterceptor(ISystemTime systemTime) => _systemTime = systemTime;

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntities(eventData.Context, _systemTime.UtcNow);

            return new ValueTask<InterceptionResult<int>>(result);
        }

        private static void UpdateAuditableEntities(DbContext dbContext, DateTime utcNow)
        {
            foreach (EntityEntry<IAuditableEntity> entityEntry in dbContext.ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue = utcNow;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(nameof(IAuditableEntity.ModifiedOnUtc)).CurrentValue = utcNow;
                }
            }
        }
    }
}
