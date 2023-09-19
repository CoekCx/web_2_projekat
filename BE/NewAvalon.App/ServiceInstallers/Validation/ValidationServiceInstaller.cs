using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.App.Abstractions;
using System.Reflection;

namespace NewAvalon.App.ServiceInstallers.Validation
{
    public class ValidationServiceInstaller : IServiceInstaller
    {
        private readonly Assembly[] _boundaryAssemblies =
        {
            typeof(UserAdministration.Boundary.AssemblyReference).Assembly,
            typeof(Catalog.Boundary.AssemblyReference).Assembly,
            typeof(Order.Boundary.AssemblyReference).Assembly,
            typeof(Notification.Boundary.AssemblyReference).Assembly
        };

        public void InstallServices(IServiceCollection services) => services.AddValidatorsFromAssemblies(_boundaryAssemblies);
    }
}
