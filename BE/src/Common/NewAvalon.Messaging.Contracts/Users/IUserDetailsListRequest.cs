using System;

namespace NewAvalon.Messaging.Contracts.Users
{
    public interface IUserDetailsListRequest
    {
        public Guid[] UserIds { get; set; }
    }
}