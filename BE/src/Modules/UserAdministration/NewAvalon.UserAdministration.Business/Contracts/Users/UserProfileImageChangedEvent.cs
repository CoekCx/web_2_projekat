using NewAvalon.Messaging.Contracts.Users;
using System;

namespace NewAvalon.UserAdministration.Business.Contracts.Users
{
    internal sealed class UserProfileImageChangedEvent : IUserProfileImageChangedEvent
    {
        public Guid OldImageId { get; set; }
    }
}