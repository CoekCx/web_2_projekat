using Microsoft.Extensions.Options;
using NewAvalon.UserAdministration.Persistence.BackgroundTasks;
using NewAvalon.UserAdministration.Persistence.Options;
using Quartz;

namespace NewAvalon.App.ServiceInstallers.BackgroundTasks.UserAdministration
{
    public sealed class UserAdministrationPublishDomainEventsJobSetup : IPostConfigureOptions<QuartzOptions>
    {
        private readonly UserAdministrationPublishDomainEventsJobOptions _options;

        public UserAdministrationPublishDomainEventsJobSetup(IOptions<UserAdministrationPublishDomainEventsJobOptions> options) =>
            _options = options.Value;

        public void PostConfigure(string name, QuartzOptions options)
        {
            var jobKey = new JobKey(nameof(UserAdministrationPublishDomainEventsJob));

            options.AddJob<UserAdministrationPublishDomainEventsJob>(builder => builder.WithIdentity(jobKey));

            options.AddTrigger(builder =>
                builder.ForJob(jobKey).WithSimpleSchedule(schedule =>
                    schedule.WithIntervalInSeconds(_options.IntervalInSeconds).RepeatForever()));
        }
    }
}
