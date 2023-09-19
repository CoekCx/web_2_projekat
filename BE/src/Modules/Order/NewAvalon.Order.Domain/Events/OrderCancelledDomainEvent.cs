using NewAvalon.Domain.Abstractions;
using System;

namespace NewAvalon.Order.Domain.Events
{
    public sealed record OrderCancelledDomainEvent(Guid OrderId) : IDomainEvent;
}
