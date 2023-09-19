using NewAvalon.Notification.Domain.EntityIdentifiers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Notification.Domain.Repositories
{
    public interface INotificationRepository
    {
        Task<List<Entities.Notification>> GetByIdsAsync(NotificationId[] notificationIds, CancellationToken cancellationToken = default);

        void Insert(Entities.Notification notification);
    }
}
