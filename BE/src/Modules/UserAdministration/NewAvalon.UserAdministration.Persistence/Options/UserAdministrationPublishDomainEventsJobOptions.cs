using NewAvalon.Persistence.Options;

namespace NewAvalon.UserAdministration.Persistence.Options
{
    public sealed class UserAdministrationPublishDomainEventsJobOptions : IPublishDomainEventsJobOptions
    {
        public int IntervalInSeconds { get; init; }

        public int BatchSize { get; init; }

        public int RetryCountThreshold { get; init; }
    }
}
