using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewAvalon.Notification.Business.Options;

namespace NewAvalon.App.ServiceInstallers.Notifications
{
    public class SendEmailNotificationJobOptionsSetup : IConfigureOptions<EmailNotificationsJobOptions>
    {
        private const string ConfigurationSectionName = "Notification:EmailNotificationsJob";
        private readonly IConfiguration _configuration;

        public SendEmailNotificationJobOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(EmailNotificationsJobOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}