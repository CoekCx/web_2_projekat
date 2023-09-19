using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace NewAvalon.App
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(WithApplicationConfiguration)
                .ConfigureWebHostDefaults(WithApplicationWebHostConfiguration);

        private static void WithApplicationWebHostConfiguration(IWebHostBuilder builder) => builder.UseStartup<Startup>();

        private static void WithApplicationConfiguration(HostBuilderContext builderContext, IConfigurationBuilder builder)
        {
            IHostEnvironment environment = builderContext.HostingEnvironment;
            builder.SetBasePath(environment.ContentRootPath);
            builder.AddJsonFile("appsettings.json", false, true);
            builder.AddJsonFile($"appsettings.{environment.EnvironmentName.ToLowerInvariant()}.json", true, true);
            builder.AddJsonFile("logger.settings.json", false, true);
            builder.AddJsonFile($"logger.settings.{environment.EnvironmentName.ToLowerInvariant()}.json", true, true);
            builder.AddJsonFile("messaging.settings.json", false, true);
            builder.AddJsonFile($"messaging.settings.{environment.EnvironmentName.ToLowerInvariant()}.json", true, true);
            builder.AddJsonFile("documentation.settings.json", false, true);
            builder.AddJsonFile($"documentation.settings.{environment.EnvironmentName.ToLowerInvariant()}.json", true, true);
            builder.AddJsonFile("configuration.settings.json", false, true);
            builder.AddJsonFile($"configuration.settings.{environment.EnvironmentName.ToLowerInvariant()}.json", true, true);

            if (!environment.IsProduction())
            {
                builder.AddUserSecrets(Assembly.GetExecutingAssembly());
            }

            builder.AddEnvironmentVariables();

            Console.WriteLine($"Environment: " + environment.EnvironmentName);
        }
    }
}
