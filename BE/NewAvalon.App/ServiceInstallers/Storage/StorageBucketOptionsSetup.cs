using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewAvalon.Storage.Infrastructure.Options;

namespace NewAvalon.App.ServiceInstallers.Storage
{
    public class StorageBucketOptionsSetup : IConfigureOptions<StorageBucketOptions>
    {
        private const string ConfigurationSectionName = "StorageBucket";
        private readonly IConfiguration _configuration;

        public StorageBucketOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(StorageBucketOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
