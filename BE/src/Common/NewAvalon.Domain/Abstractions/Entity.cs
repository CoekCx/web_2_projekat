using System;

namespace NewAvalon.Domain.Abstractions
{
    /// <summary>
    /// Represents the base class for all entities.
    /// </summary>
    /// <typeparam name="TEntityId">The entity identifier type.</typeparam>
    public abstract class Entity<TEntityId> : IEquatable<Entity<TEntityId>>
        where TEntityId : class, IEntityId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        protected Entity(TEntityId id) => Id = id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        protected Entity()
        {
        }

        /// <summary>
        /// Gets the entity identifier.
        /// </summary>
        public TEntityId Id { get; private set; }

        public static bool operator ==(Entity<TEntityId> a, Entity<TEntityId> b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TEntityId> a, Entity<TEntityId> b) => !(a == b);

        /// <inheritdoc />
        public bool Equals(Entity<TEntityId> other)
        {
            if (other is null)
            {
                return false;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            if (obj is not Entity<TEntityId> otherEntity)
            {
                return false;
            }

            return Id.Equals(otherEntity.Id);
        }

        /// <inheritdoc />
        public override int GetHashCode() => Id.GetHashCode();
    }
}
