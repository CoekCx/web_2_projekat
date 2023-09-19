using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewAvalon.UserAdministration.Persistence.Options;

namespace NewAvalon.App.ServiceInstallers.Persistence
{
    internal class UserAdministrationDatabaseOptionsSetup : IConfigureOptions<UserAdministrationDatabaseOptions>
    {
        private const string ConfigurationSectionName = "UserAdministration:DatabaseConnection";
        private readonly IConfiguration _configuration;

        public UserAdministrationDatabaseOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(UserAdministrationDatabaseOptions options) =>
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
