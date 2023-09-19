using NewAvalon.Abstractions.Messaging;
using NewAvalon.Domain.Abstractions;
using NewAvalon.Domain.Enums;
using NewAvalon.UserAdministration.Business.Contracts.Notifications;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Events;
using NewAvalon.UserAdministration.Domain.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Events
{
    public class DealerUserApprovedDomainEventHandler : IDomainEventHandler<DealerUserApprovedDomainEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventPublisher _publisher;

        public DealerUserApprovedDomainEventHandler(
            IUserRepository userRepository,
            IEventPublisher publisher)
        {
            _userRepository = userRepository;
            _publisher = publisher;
        }

        public async Task Handle(DealerUserApprovedDomainEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(new UserId(notification.UserId), cancellationToken);

            if (user is null)
            {
                return;
            }

            var notificationCreated = new NotificationCreatedEvent
            {
                UserId = notification.UserId,
                Title = "Dealer user approved",
                Content = $"{user.FirstName} {user.LastName} has approved.",
                DeliveryMechanism = DeliveryMechanism.Email,
                NotificationType = NotificationType.DealerUserApproved,
                Details = new Dictionary<string, object>
                {
                    { "userId", user.Id.Value },
                }
            };

            await _publisher.PublishAsync(notificationCreated, cancellationToken);
        }
    }
}
