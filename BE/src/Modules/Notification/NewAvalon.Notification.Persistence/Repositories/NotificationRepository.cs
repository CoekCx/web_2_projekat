using Microsoft.EntityFrameworkCore;
using NewAvalon.Notification.Domain.EntityIdentifiers;
using NewAvalon.Notification.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Notification.Persistence.Repositories
{
    internal sealed class NotificationRepository : INotificationRepository
    {
        private readonly NotificationDbContext _dbContext;

        public NotificationRepository(NotificationDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<Domain.Entities.Notification>> GetByIdsAsync(
            NotificationId[] notificationIds,
            CancellationToken cancellationToken = default) =>
            await _dbContext.Set<Domain.Entities.Notification>()
                .Where(notification => notificationIds.Contains(notification.Id))
                .ToListAsync(cancellationToken);

        public void Insert(Domain.Entities.Notification notification) => _dbContext.Set<Domain.Entities.Notification>().Add(notification);
    }
}
