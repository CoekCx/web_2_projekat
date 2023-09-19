using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewAvalon.UserAdministration.Persistence.Options;

namespace NewAvalon.App.ServiceInstallers.BackgroundTasks.UserAdministration
{
    public sealed class UserAdministrationPublishDomainEventsJobOptionsSetup
        : IConfigureOptions<UserAdministrationPublishDomainEventsJobOptions>
    {
        private const string ConfigurationSectionName = "UserAdministration:PublishDomainEventsJob";
        private readonly IConfiguration _configuration;

        public UserAdministrationPublishDomainEventsJobOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(UserAdministrationPublishDomainEventsJobOptions options) =>
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
