using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewAvalon.Storage.Persistence.Options;

namespace NewAvalon.App.ServiceInstallers.Persistence
{
    public class StorageDatabaseOptionsSetup : IConfigureOptions<StorageDatabaseOptions>
    {
        private const string ConfigurationSectionName = "Storage:DatabaseConnection";
        private readonly IConfiguration _configuration;

        public StorageDatabaseOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(StorageDatabaseOptions options) =>
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
