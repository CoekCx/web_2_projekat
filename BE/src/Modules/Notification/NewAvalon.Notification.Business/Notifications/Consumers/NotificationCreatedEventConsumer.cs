using MassTransit;
using NewAvalon.Messaging.Contracts.Notifications;
using NewAvalon.Notification.Domain.EntityIdentifiers;
using NewAvalon.Notification.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace NewAvalon.Notification.Business.Notifications.Consumers
{
    public sealed class NotificationCreatedEventConsumer : IConsumer<INotificationCreatedEvent>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationUnitOfWork _unitOfWork;

        public NotificationCreatedEventConsumer(
            INotificationRepository notificationRepository,
            INotificationUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<INotificationCreatedEvent> context)
        {
            INotificationCreatedEvent notificationCreated = context.Message;

            Domain.Entities.Notification notification = notificationCreated.UserId.HasValue
                ? new Domain.Entities.Notification(
                    new NotificationId(Guid.NewGuid()),
                    notificationCreated.UserId.Value,
                    notificationCreated.DeliveryMechanism,
                    notificationCreated.NotificationType,
                    notificationCreated.Title,
                    notificationCreated.Content,
                    notificationCreated.Details)
                : new Domain.Entities.Notification(
                    new NotificationId(Guid.NewGuid()),
                    notificationCreated.Email,
                    notificationCreated.DeliveryMechanism,
                    notificationCreated.NotificationType,
                    notificationCreated.Title,
                    notificationCreated.Content,
                    notificationCreated.Details);

            _notificationRepository.Insert(notification);

            /*            var userDetailsRequest = new UserDetailsRequest()
                        {
                            Id = notification.UserId.Value
                        };

                        var userDetails =
                            await _userDetailsRequestClient.GetResponse<IUserDetailsResponse>(userDetailsRequest,
                                context.CancellationToken);

                        await _sendGridClient.SendEmailAsync(CreateSendGridMessage(userDetails.Message, notification.Type));*/

            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }
}
