using NewAvalon.Persistence.Options;

namespace NewAvalon.Catalog.Persistence.Options
{
    public sealed class CatalogPublishDomainEventsJobOptions : IPublishDomainEventsJobOptions
    {
        public int IntervalInSeconds { get; init; }

        public int BatchSize { get; init; }

        public int RetryCountThreshold { get; init; }
    }
}
