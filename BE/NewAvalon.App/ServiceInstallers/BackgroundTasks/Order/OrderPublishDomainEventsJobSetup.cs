using Microsoft.Extensions.Options;
using NewAvalon.Order.Persistence.BackgroundTasks;
using NewAvalon.Order.Persistence.Options;
using Quartz;

namespace NewAvalon.App.ServiceInstallers.BackgroundTasks.Order
{
    public sealed class OrderPublishDomainEventsJobSetup : IPostConfigureOptions<QuartzOptions>
    {
        private readonly OrderPublishDomainEventsJobOptions _options;

        public OrderPublishDomainEventsJobSetup(IOptions<OrderPublishDomainEventsJobOptions> options) =>
            _options = options.Value;

        public void PostConfigure(string name, QuartzOptions options)
        {
            var jobKey = new JobKey(nameof(OrderPublishDomainEventsJob));

            options.AddJob<OrderPublishDomainEventsJob>(builder => builder.WithIdentity(jobKey));

            options.AddTrigger(builder =>
                builder.ForJob(jobKey).WithSimpleSchedule(schedule =>
                    schedule.WithIntervalInSeconds(_options.IntervalInSeconds).RepeatForever()));
        }
    }
}
