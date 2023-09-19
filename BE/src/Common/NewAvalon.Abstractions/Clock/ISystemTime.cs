using System;

namespace NewAvalon.Abstractions.Clock
{
    /// <summary>
    /// Represents the interface for getting the current system time.
    /// </summary>
    public interface ISystemTime
    {
        /// <summary>
        /// Gets the current date and time in the UTC time zone.
        /// </summary>
        DateTime UtcNow { get; }
    }
}
