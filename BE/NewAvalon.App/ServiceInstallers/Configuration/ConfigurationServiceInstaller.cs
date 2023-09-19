using Microsoft.Extensions.DependencyInjection;
using NewAvalon.App.Abstractions;

namespace NewAvalon.App.ServiceInstallers.Configuration
{
    public class ConfigurationServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services.ConfigureOptions<EmailTemplatesOptionsSetup>();
        }
    }
}
