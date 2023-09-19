using Microsoft.Extensions.Options;
using NewAvalon.Order.Persistence.BackgroundTasks;
using Quartz;

namespace NewAvalon.App.ServiceInstallers.BackgroundTasks.Order
{
    public class OrderDeliveryJobSetup : IPostConfigureOptions<QuartzOptions>
    {
        public void PostConfigure(string name, QuartzOptions options)
        {
            var jobKey = new JobKey(nameof(OrderDeliveryJob));

            options.AddJob<OrderDeliveryJob>(builder => builder.WithIdentity(jobKey));

            options.AddTrigger(builder =>
                builder.ForJob(jobKey).WithSimpleSchedule(schedule =>
                schedule.WithIntervalInSeconds(20).RepeatForever()));
        }
    }
}
