using Microsoft.Extensions.DependencyInjection;
using NewAvalon.App.Abstractions;
using NewAvalon.App.ServiceInstallers.BackgroundTasks.Catalog;
using NewAvalon.App.ServiceInstallers.BackgroundTasks.Notifications;
using NewAvalon.App.ServiceInstallers.BackgroundTasks.Order;
using NewAvalon.App.ServiceInstallers.BackgroundTasks.UserAdministration;
using NewAvalon.App.ServiceInstallers.Notifications;
using Quartz;

namespace NewAvalon.App.ServiceInstallers.BackgroundTasks
{
    public sealed class BackgroundTasksServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            InstallOptions(services);

            InstallCore(services);
        }

        private static void InstallOptions(IServiceCollection services)
        {
            services.ConfigureOptions<QuartzHostedServiceOptionsSetup>();

            services.ConfigureOptions<UserAdministrationPublishDomainEventsJobOptionsSetup>();

            services.ConfigureOptions<UserAdministrationPublishDomainEventsJobSetup>();

            services.ConfigureOptions<OrderPublishDomainEventsJobOptionsSetup>();

            services.ConfigureOptions<OrderPublishDomainEventsJobSetup>();

            services.ConfigureOptions<CatalogPublishDomainEventsJobOptionsSetup>();

            services.ConfigureOptions<CatalogPublishDomainEventsJobSetup>();

            services.ConfigureOptions<SendEmailNotificationJobOptionsSetup>();

            services.ConfigureOptions<SendEmailNotificationJobSetup>();

            services.ConfigureOptions<OrderDeliveryJobSetup>();
        }

        public void InstallCore(IServiceCollection services)
        {
            services.AddQuartz(configurator => configurator.UseMicrosoftDependencyInjectionJobFactory());

            services.AddQuartzHostedService();
        }
    }
}
