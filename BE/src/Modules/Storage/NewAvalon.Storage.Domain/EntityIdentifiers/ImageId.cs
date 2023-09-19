using NewAvalon.Domain.Abstractions;
using System;

namespace NewAvalon.Storage.Domain.EntityIdentifiers
{
    public sealed record ImageId(Guid Value) : IEntityId;
}
