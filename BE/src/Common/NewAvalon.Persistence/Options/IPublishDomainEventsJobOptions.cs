namespace NewAvalon.Persistence.Options
{
    public interface IPublishDomainEventsJobOptions
    {
        public int IntervalInSeconds { get; init; }

        public int BatchSize { get; init; }

        public int RetryCountThreshold { get; init; }
    }
}
