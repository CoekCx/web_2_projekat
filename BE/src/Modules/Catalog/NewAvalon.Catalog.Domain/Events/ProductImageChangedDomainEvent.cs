using NewAvalon.Domain.Abstractions;
using System;

namespace NewAvalon.Catalog.Domain.Events
{
    public sealed record ProductImageChangedDomainEvent(Guid OldImageId) : IDomainEvent;
}
