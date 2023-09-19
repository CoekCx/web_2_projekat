using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NewAvalon.App.Abstractions
{
    /// <summary>
    /// Represents the service installer factory.
    /// </summary>
    internal static class ServiceInstallerFactory
    {
        /// <summary>
        /// Gets all of the service installers from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to scan for installers.</param>
        /// <returns>The list of found service installer instances.</returns>
        internal static IEnumerable<IServiceInstaller> GetServiceInstallersFromAssembly(Assembly assembly)
        {
            IEnumerable<IServiceInstaller> serviceInstallers = assembly.DefinedTypes.Where(IsAssignableToServiceInstaller())
                .Select(x => Activator.CreateInstance(x) as IServiceInstaller);

            return serviceInstallers;
        }

        private static Func<TypeInfo, bool> IsAssignableToServiceInstaller() =>
            x => typeof(IServiceInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract;
    }
}
