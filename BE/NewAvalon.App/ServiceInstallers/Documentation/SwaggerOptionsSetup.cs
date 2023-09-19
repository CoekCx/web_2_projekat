using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace NewAvalon.App.ServiceInstallers.Documentation
{
    internal class SwaggerOptionsSetup : IConfigureOptions<SwaggerGenOptions>
    {
        private const string ConfigurationSectionName = "Documentation:Generator:Security";

        private readonly Assembly[] _presentationAssemblies =
        {
            typeof(UserAdministration.Presentation.AssemblyReference).Assembly,
            typeof(Catalog.Presentation.AssemblyReference).Assembly,
            typeof(Notification.Presentation.AssemblyReference).Assembly,
            typeof(Order.Presentation.AssemblyReference).Assembly,
        };

        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "1.0.0",
                Title = "New Avalon API",
                Description = "New Avalon API built with .NET 5.0."
            });

            /*            foreach (string presentationDocumentationFilePath in _presentationAssemblies.Select(CreateDocumentationFilePath))
                        {
                            options.IncludeXmlComments(presentationDocumentationFilePath);
                        }*/
        }

        private static string CreateDocumentationFilePath(Assembly assembly)
        {
            string presentationDocumentationFile = $"{assembly.GetName().Name}.xml";

            string presentationDocumentationFilePath = Path.Combine(AppContext.BaseDirectory, presentationDocumentationFile);

            return presentationDocumentationFilePath;
        }
    }
}
