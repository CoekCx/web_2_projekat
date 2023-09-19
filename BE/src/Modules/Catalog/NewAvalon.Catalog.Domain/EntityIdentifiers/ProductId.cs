using NewAvalon.Domain.Abstractions;
using System;

namespace NewAvalon.Catalog.Domain.EntityIdentifiers
{
    public sealed record ProductId(Guid Value) : IEntityId;
}
