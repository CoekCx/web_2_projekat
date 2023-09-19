using Microsoft.Extensions.DependencyInjection;
using NewAvalon.App.Abstractions;
using NewAvalon.App.Extensions;

namespace NewAvalon.App.ServiceInstallers.Infrastructure
{
    public class InfrastructureServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services.AddTransientServicesAsMatchingInterfaces(typeof(NewAvalon.Infrastructure.AssemblyReference).Assembly);

            services.AddScopedServicesAsMatchingInterfaces(typeof(NewAvalon.Infrastructure.AssemblyReference).Assembly);

            services.AddScopedServicesAsMatchingInterfaces(typeof(Catalog.Persistence.AssemblyReference).Assembly);

            services.AddTransientServicesAsMatchingInterfaces(typeof(Notification.Infrastructure.AssemblyReference).Assembly);

            services.AddScopedServicesAsMatchingInterfaces(typeof(UserAdministration.Infrastructure.AssemblyReference).Assembly);

            services.AddTransientServicesAsMatchingInterfaces(typeof(UserAdministration.Infrastructure.AssemblyReference).Assembly);
        }
    }
}
