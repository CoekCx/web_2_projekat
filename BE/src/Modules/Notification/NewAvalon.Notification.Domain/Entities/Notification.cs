using NewAvalon.Domain.Abstractions;
using NewAvalon.Domain.Enums;
using NewAvalon.Notification.Domain.EntityIdentifiers;
using System;
using System.Collections.Generic;

namespace NewAvalon.Notification.Domain.Entities
{
    public sealed class Notification : Entity<NotificationId>, IAuditableEntity
    {
        public Notification(
            NotificationId id,
            Guid userId,
            DeliveryMechanism deliveryMechanism,
            NotificationType type,
            string title,
            string content,
            Dictionary<string, object> details)
            : base(id)
        {
            UserId = userId;
            DeliveryMechanism = deliveryMechanism;
            Type = type;
            Title = title;
            Content = content;
            Details = details;
        }

        public Notification(
            NotificationId id,
            string email,
            DeliveryMechanism deliveryMechanism,
            NotificationType type,
            string title,
            string content,
            Dictionary<string, object> details)
            : base(id)
        {
            Email = email;
            DeliveryMechanism = deliveryMechanism;
            Type = type;
            Title = title;
            Content = content;
            Details = details;
        }

        private Notification()
        {
        }

        public Guid? UserId { get; private set; }

        public string Email { get; private set; }

        public DeliveryMechanism DeliveryMechanism { get; private set; }

        public NotificationType Type { get; private set; }

        public string Title { get; private set; }

        public string Content { get; private set; }

        public Dictionary<string, object> Details { get; private set; } = new();

        public bool Published { get; private set; }

        public DateTime? PublishedOnUtc { get; private set; }

        public bool Failed { get; private set; }

        public DateTime? FailedOnUtc { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }

        public void Publish(DateTime utcNow)
        {
            Published = true;
            PublishedOnUtc = utcNow;
        }

        public void Fail(DateTime utcNow)
        {
            Failed = true;
            FailedOnUtc = utcNow;
        }
    }
}
