using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NewAvalon.App.Abstractions;
using System.Collections.Generic;

namespace NewAvalon.App.ServiceInstallers.Documentation
{
    public class DocumentationServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            InstallOptions(services);

            InstallCore(services);
        }

        private static void InstallOptions(IServiceCollection services)
        {
            services.ConfigureOptions<SwaggerOptionsSetup>();

            services.ConfigureOptions<SwaggerUIOptionsSetup>();
        }

        private static void InstallCore(IServiceCollection services) => services.AddSwaggerGen(genOpts =>
        {
            genOpts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            genOpts.AddSecurityRequirement(
                new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "Bearer",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
        });
    }
}
