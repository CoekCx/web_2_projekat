using NewAvalon.Messaging.Contracts.Users;
using System;

namespace NewAvalon.Order.Business.Contracts.Users
{
    internal sealed class UserDetailsRequest : IUserDetailsRequest
    {
        public Guid Id { get; set; }
    }
}
