using NewAvalon.Domain.Abstractions;
using System;

namespace NewAvalon.UserAdministration.Domain.EntityIdentifiers
{
    public sealed record UserId(Guid Value) : IEntityId;
}
