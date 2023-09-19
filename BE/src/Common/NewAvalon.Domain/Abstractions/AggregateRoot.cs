using System.Collections.Generic;
using System.Linq;

namespace NewAvalon.Domain.Abstractions
{
    /// <summary>
    /// Represents the base class for all aggregate roots.
    /// </summary>
    /// <typeparam name="TEntityId">The entity identifier.</typeparam>
    public abstract class AggregateRoot<TEntityId> : Entity<TEntityId>, IAggregateRoot
        where TEntityId : class, IEntityId
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot{TEntityId}"/> class.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        protected AggregateRoot(TEntityId id)
            : base(id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot{TEntityId}"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        protected AggregateRoot()
        {
        }

        /// <summary>
        /// Gets the domain events.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.ToList();

        /// <summary>
        /// Clears all of the previously raised domain events.
        /// </summary>
        public void ClearDomainEvents() => _domainEvents.Clear();

        public void ClearDomainEvents<T>() => _domainEvents.OfType<T>()
            .ToList()
            .Clear();

        /// <summary>
        /// Raises the specified domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    }
}
