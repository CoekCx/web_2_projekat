using System;

namespace NewAvalon.Messaging.Contracts.Permissions
{
    public interface IGetPermissionsRequest
    {
        Guid UserId { get; set; }
    }
}
