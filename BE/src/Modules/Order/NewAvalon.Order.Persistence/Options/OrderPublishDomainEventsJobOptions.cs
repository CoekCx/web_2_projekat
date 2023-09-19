using NewAvalon.Persistence.Options;

namespace NewAvalon.Order.Persistence.Options
{
    public sealed class OrderPublishDomainEventsJobOptions : IPublishDomainEventsJobOptions
    {
        public int IntervalInSeconds { get; init; }

        public int BatchSize { get; init; }

        public int RetryCountThreshold { get; init; }
    }
}
