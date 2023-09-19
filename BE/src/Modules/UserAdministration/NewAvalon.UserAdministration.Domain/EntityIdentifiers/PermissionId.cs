using NewAvalon.Domain.Abstractions;

namespace NewAvalon.UserAdministration.Domain.EntityIdentifiers
{
    public sealed record PermissionId(int Value) : IEntityId;
}
