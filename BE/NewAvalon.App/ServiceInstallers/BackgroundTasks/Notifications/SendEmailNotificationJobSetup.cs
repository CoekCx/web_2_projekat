using Microsoft.Extensions.Options;
using NewAvalon.Notification.Business.Options;
using NewAvalon.Notification.Infrastructure.BackgroundTasks;
using Quartz;

namespace NewAvalon.App.ServiceInstallers.BackgroundTasks.Notifications
{
    public sealed class SendEmailNotificationJobSetup : IPostConfigureOptions<QuartzOptions>
    {
        private readonly EmailNotificationsJobOptions _options;

        public SendEmailNotificationJobSetup(IOptions<EmailNotificationsJobOptions> options) => _options = options.Value;

        public void PostConfigure(string name, QuartzOptions options)
        {
            var jobKey = new JobKey(nameof(SendEmailNotificationsJob));

            options.AddJob<SendEmailNotificationsJob>(builder => builder.WithIdentity(jobKey));

            options.AddTrigger(builder =>
                builder.ForJob(jobKey)
                    .WithSimpleSchedule(scheduleBuilder =>
                        scheduleBuilder.WithIntervalInSeconds(_options.IntervalInSeconds).RepeatForever()));
        }
    }
}