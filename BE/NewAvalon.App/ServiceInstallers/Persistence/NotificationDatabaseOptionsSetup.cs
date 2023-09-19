using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewAvalon.Notification.Persistence.Options;

namespace NewAvalon.App.ServiceInstallers.Persistence
{
    public class NotificationDatabaseOptionsSetup : IConfigureOptions<NotificationDatabaseOptions>
    {
        private const string ConfigurationSectionName = "Notification:DatabaseConnection";
        private readonly IConfiguration _configuration;

        public NotificationDatabaseOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(NotificationDatabaseOptions options) =>
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}