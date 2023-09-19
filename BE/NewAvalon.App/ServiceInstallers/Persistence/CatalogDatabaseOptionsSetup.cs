using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewAvalon.Catalog.Persistence.Options;

namespace NewAvalon.App.ServiceInstallers.Persistence
{
    internal class CatalogDatabaseOptionsSetup : IConfigureOptions<CatalogDatabaseOptions>
    {
        private const string ConfigurationSectionName = "Catalog:DatabaseConnection";
        private readonly IConfiguration _configuration;

        public CatalogDatabaseOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(CatalogDatabaseOptions options) =>
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
