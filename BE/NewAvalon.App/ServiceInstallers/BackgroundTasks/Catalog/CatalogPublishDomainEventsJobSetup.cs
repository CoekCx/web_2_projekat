using Microsoft.Extensions.Options;
using NewAvalon.Catalog.Persistence.BackgroundTasks;
using NewAvalon.Catalog.Persistence.Options;
using Quartz;

namespace NewAvalon.App.ServiceInstallers.BackgroundTasks.Catalog
{
    public sealed class CatalogPublishDomainEventsJobSetup : IPostConfigureOptions<QuartzOptions>
    {
        private readonly CatalogPublishDomainEventsJobOptions _options;

        public CatalogPublishDomainEventsJobSetup(IOptions<CatalogPublishDomainEventsJobOptions> options) =>
            _options = options.Value;

        public void PostConfigure(string name, QuartzOptions options)
        {
            var jobKey = new JobKey(nameof(CatalogPublishDomainEventsJob));

            options.AddJob<CatalogPublishDomainEventsJob>(builder => builder.WithIdentity(jobKey));

            options.AddTrigger(builder =>
                builder.ForJob(jobKey).WithSimpleSchedule(schedule =>
                    schedule.WithIntervalInSeconds(_options.IntervalInSeconds).RepeatForever()));
        }
    }
}