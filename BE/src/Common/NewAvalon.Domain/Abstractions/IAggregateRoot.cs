using System.Collections.Generic;

namespace NewAvalon.Domain.Abstractions
{
    /// <summary>
    /// Represents the aggregate root interface.
    /// </summary>
    public interface IAggregateRoot
    {
        /// <summary>
        /// Gets the domain events.
        /// </summary>
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        /// <summary>
        /// Clears all of the previously raised domain events.
        /// </summary>
        void ClearDomainEvents();

        /// <summary>
        /// Clears all of the previously raised domain events by type.
        /// </summary>
        /// <typeparam name="T">Domain event Type.</typeparam>
        void ClearDomainEvents<T>();
    }
}
