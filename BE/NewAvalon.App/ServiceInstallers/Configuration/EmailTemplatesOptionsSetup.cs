using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewAvalon.Notification.Business.Options;

namespace NewAvalon.App.ServiceInstallers.Configuration
{
    public class EmailTemplatesOptionsSetup : IConfigureOptions<EmailTemplatesOptions>
    {
        private const string ConfigurationSectionName = "EmailTemplates";
        private readonly IConfiguration _configuration;

        public EmailTemplatesOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(EmailTemplatesOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
