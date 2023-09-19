using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Abstractions.Behaviors;
using NewAvalon.App.Abstractions;
using System.Reflection;

namespace NewAvalon.App.ServiceInstallers.Mediator
{
    public class MediatorServiceInstaller : IServiceInstaller
    {
        private readonly Assembly[] _businessAssemblies =
        {
            typeof(UserAdministration.Business.AssemblyReference).Assembly,
            typeof(Catalog.Business.AssemblyReference).Assembly,
            typeof(Order.Business.AssemblyReference).Assembly,
            typeof(Notification.Business.AssemblyReference).Assembly,
            typeof(NewAvalon.Storage.Business.AssemblyReference).Assembly,
        };

        public void InstallServices(IServiceCollection services)
        {
            services.AddMediatR(_businessAssemblies);

            AddPipelineBehaviors(services);
        }

        private static void AddPipelineBehaviors(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
        }
    }
}
