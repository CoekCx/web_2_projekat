using Microsoft.Extensions.DependencyInjection;
using NewAvalon.App.Abstractions;

namespace NewAvalon.App.ServiceInstallers.HealthCheck
{
    public class HealthCheckServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services) => services.AddHealthChecks();
    }
}
