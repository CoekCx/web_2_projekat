using NewAvalon.Messaging.Contracts.Users;
using System;

namespace NewAvalon.Order.Persistence.Contracts.Users
{
    internal sealed class UserDetailsListRequest : IUserDetailsListRequest
    {
        public Guid[] UserIds { get; set; }
    }
}
