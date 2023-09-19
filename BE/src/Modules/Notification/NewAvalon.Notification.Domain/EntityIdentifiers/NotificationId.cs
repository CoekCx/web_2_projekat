using NewAvalon.Domain.Abstractions;
using System;

namespace NewAvalon.Notification.Domain.EntityIdentifiers
{
    public sealed record NotificationId(Guid Value) : IEntityId;
}
