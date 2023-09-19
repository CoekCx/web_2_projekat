using NewAvalon.Domain.Enums;
using NewAvalon.Messaging.Contracts.Notifications;
using System;
using System.Collections.Generic;

namespace NewAvalon.UserAdministration.Business.Contracts.Notifications
{
    internal sealed class NotificationCreatedEvent : INotificationCreatedEvent
    {
        public Guid? UserId { get; set; }

        public string Email { get; set; }
        public DeliveryMechanism DeliveryMechanism { get; set; }

        public NotificationType NotificationType { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public Dictionary<string, object> Details { get; set; }
    }
}
