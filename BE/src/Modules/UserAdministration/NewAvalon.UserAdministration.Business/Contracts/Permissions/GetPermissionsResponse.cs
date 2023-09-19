using NewAvalon.Messaging.Contracts.Permissions;

namespace NewAvalon.UserAdministration.Business.Contracts.Permissions
{
    internal sealed class GetPermissionsResponse : IGetPermissionsResponse
    {
        public string[] PermissionNames { get; set; }
    }
}
