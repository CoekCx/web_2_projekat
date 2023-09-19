using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Abstractions.ServiceLifetimes;
using NewAvalon.App.Abstractions;
using Scrutor;
using System.Linq;
using System.Reflection;

namespace NewAvalon.App.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static void InstallServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var serviceInstallers = ServiceInstallerFactory.GetServiceInstallersFromAssembly(assembly).ToList();

            serviceInstallers.ForEach(x => x.InstallServices(services));
        }

        internal static IServiceCollection AddTransientServicesAsMatchingInterfaces(this IServiceCollection services, Assembly assembly) =>
            services.Scan(scan =>
                scan.FromAssemblies(assembly)
                    .AddClasses(filter => filter.AssignableTo<ITransient>(), false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                    .AsMatchingInterface()
                    .WithTransientLifetime());

        internal static IServiceCollection AddScopedServicesAsMatchingInterfaces(this IServiceCollection services, Assembly assembly) =>
            services.Scan(scan =>
                scan.FromAssemblies(assembly)
                    .AddClasses(filter => filter.AssignableTo<IScoped>(), false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                    .AsMatchingInterface()
                    .WithTransientLifetime());

        internal static IServiceCollection AddSingletonServicesAsMatchingInterfaces(this IServiceCollection services, Assembly assembly) =>
            services.Scan(scan =>
                scan.FromAssemblies(assembly)
                    .AddClasses(filter => filter.AssignableTo<ISingleton>(), false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                    .AsMatchingInterface()
                    .WithSingletonLifetime());
    }
}
