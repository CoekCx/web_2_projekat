using MediatR;

namespace NewAvalon.Domain.Abstractions
{
    /// <summary>
    /// Represents the domain event marker interface.
    /// </summary>
    public interface IDomainEvent : INotification
    {
    }
}
