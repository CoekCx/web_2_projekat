using NewAvalon.Abstractions.Messaging;
using NewAvalon.Domain.Abstractions;
using NewAvalon.Messaging.Contracts.Users;
using NewAvalon.UserAdministration.Business.Contracts.Users;
using NewAvalon.UserAdministration.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Events
{
    internal sealed class UserProfileImageChangedDomainEventHandler : IDomainEventHandler<UserProfileImageChangedDomainEvent>
    {
        private readonly IEventPublisher _eventPublisher;

        public UserProfileImageChangedDomainEventHandler(IEventPublisher eventPublisher) => _eventPublisher = eventPublisher;

        public async Task Handle(UserProfileImageChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var @event = new UserProfileImageChangedEvent
            {
                OldImageId = notification.OldImageId
            };

            await _eventPublisher.PublishAsync<IUserProfileImageChangedEvent>(@event, cancellationToken);
        }
    }
}
