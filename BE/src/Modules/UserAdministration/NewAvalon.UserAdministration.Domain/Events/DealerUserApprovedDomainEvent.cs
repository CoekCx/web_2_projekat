using NewAvalon.Domain.Abstractions;
using System;

namespace NewAvalon.UserAdministration.Domain.Events
{
    public sealed record DealerUserApprovedDomainEvent(Guid UserId) : IDomainEvent;
}
