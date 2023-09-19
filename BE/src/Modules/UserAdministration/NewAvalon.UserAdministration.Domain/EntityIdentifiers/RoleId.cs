using NewAvalon.Domain.Abstractions;

namespace NewAvalon.UserAdministration.Domain.EntityIdentifiers
{
    public sealed record RoleId(int Value) : IEntityId;
}
