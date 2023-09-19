using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewAvalon.Catalog.Persistence.Options;

namespace NewAvalon.App.ServiceInstallers.BackgroundTasks.Catalog
{
    public sealed class CatalogPublishDomainEventsJobOptionsSetup
        : IConfigureOptions<CatalogPublishDomainEventsJobOptions>
    {
        private const string ConfigurationSectionName = "Catalog:PublishDomainEventsJob";
        private readonly IConfiguration _configuration;

        public CatalogPublishDomainEventsJobOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(CatalogPublishDomainEventsJobOptions options) =>
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
