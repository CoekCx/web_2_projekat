using NewAvalon.UserAdministration.Domain.EntityIdentifiers;

namespace NewAvalon.UserAdministration.Business.Permissions.Consumers
{
    public sealed record GetPermissionsRequest(UserId UserId);
}
