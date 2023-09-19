using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewAvalon.Order.Persistence.Options;

namespace NewAvalon.App.ServiceInstallers.Persistence
{
    internal class OrderDatabaseOptionsSetup : IConfigureOptions<OrderDatabaseOptions>
    {
        private const string ConfigurationSectionName = "Order:DatabaseConnection";
        private readonly IConfiguration _configuration;

        public OrderDatabaseOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(OrderDatabaseOptions options) =>
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
