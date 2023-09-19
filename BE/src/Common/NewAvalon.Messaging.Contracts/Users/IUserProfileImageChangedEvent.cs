using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Messaging.Contracts.Users
{
    public interface IUserProfileImageChangedEvent : IEvent
    {
        Guid OldImageId { get; set; }
    }
}
