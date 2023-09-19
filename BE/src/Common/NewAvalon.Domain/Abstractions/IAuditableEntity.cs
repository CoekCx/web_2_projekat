using System;

namespace NewAvalon.Domain.Abstractions
{
    /// <summary>
    /// Represents the auditable entity interface.
    /// </summary>
    public interface IAuditableEntity
    {
        /// <summary>
        /// Gets the created on date and time in the UTC time zone.
        /// </summary>
        DateTime CreatedOnUtc { get; }

        /// <summary>
        /// Gets the modified on date and time in the UTC time zone.
        /// </summary>
        DateTime? ModifiedOnUtc { get; }
    }
}
