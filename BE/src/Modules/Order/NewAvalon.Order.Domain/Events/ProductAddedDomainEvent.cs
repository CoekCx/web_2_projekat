using NewAvalon.Domain.Abstractions;
using NewAvalon.Order.Domain.EntityIdentifiers;

namespace NewAvalon.Order.Domain.Events
{
    public sealed record ProductAddedDomainEvent(ProductId ProductId) : IDomainEvent;
}
