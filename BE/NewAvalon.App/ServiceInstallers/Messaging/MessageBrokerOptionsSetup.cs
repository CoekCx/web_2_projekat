using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewAvalon.Infrastructure.Messaging.Options;

namespace NewAvalon.App.ServiceInstallers.Messaging
{
    public class MessageBrokerOptionsSetup : IConfigureOptions<MessageBrokerOptions>
    {
        private const string ConfigurationSectionName = "MessageBroker";
        private readonly IConfiguration _configuration;

        public MessageBrokerOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(MessageBrokerOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
