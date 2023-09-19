using System;

namespace NewAvalon.Messaging.Contracts.Users
{
    public interface IUserDetailsRequest
    {
        Guid Id { get; set; }
    }
}
