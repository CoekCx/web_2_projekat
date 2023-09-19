using Microsoft.Extensions.DependencyInjection;

namespace NewAvalon.App.Abstractions
{
    internal interface IServiceInstaller
    {
        void InstallServices(IServiceCollection services);
    }
}
