using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace NewAvalon.App.ServiceInstallers.Documentation
{
    internal class SwaggerUIOptionsSetup : IConfigureOptions<SwaggerUIOptions>
    {
        private const string ConfigurationSectionName = "Documentation:Ui";
        private readonly IConfiguration _configuration;

        public SwaggerUIOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(SwaggerUIOptions options)
        {
            options.DisplayRequestDuration();

            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}
