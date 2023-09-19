using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewAvalon.Order.Persistence.Options;

namespace NewAvalon.App.ServiceInstallers.BackgroundTasks.Order
{
    public sealed class OrderPublishDomainEventsJobOptionsSetup
        : IConfigureOptions<OrderPublishDomainEventsJobOptions>
    {
        private const string ConfigurationSectionName = "Order:PublishDomainEventsJob";
        private readonly IConfiguration _configuration;

        public OrderPublishDomainEventsJobOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(OrderPublishDomainEventsJobOptions options) =>
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
