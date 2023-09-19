using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.App.Abstractions;
using NewAvalon.App.Middlewares;

namespace NewAvalon.App.ServiceInstallers.Mvc
{
    public class MvcServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            InstallOptions(services);

            InstallCore(services);
        }

        private static void InstallOptions(IServiceCollection services) => services.ConfigureOptions<RouteOptionsSetup>();

        private static void InstallCore(IServiceCollection services)
        {
            services.AddRouting()
                .AddControllers()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
                .AddApplicationPart(typeof(Catalog.Presentation.AssemblyReference).Assembly)
                .AddApplicationPart(typeof(UserAdministration.Presentation.AssemblyReference).Assembly)
                .AddApplicationPart(typeof(Notification.Presentation.AssemblyReference).Assembly)
                .AddApplicationPart(typeof(Order.Presentation.AssemblyReference).Assembly)
                .AddApplicationPart(typeof(NewAvalon.Storage.Presentation.AssemblyReference).Assembly);

            services.AddHttpContextAccessor();

            services.AddTransient<ExceptionHandlerMiddleware>();

            services.AddResponseCompression(o => o.EnableForHttps = true);
        }
    }
}
