using NewAvalon.Abstractions.Clock;
using NewAvalon.Abstractions.ServiceLifetimes;
using System;

namespace NewAvalon.Infrastructure.Clock
{
    internal sealed class SystemTime : ISystemTime, ITransient
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
