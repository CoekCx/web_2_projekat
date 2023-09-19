using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewAvalon.App.Abstractions;
using NewAvalon.App.Extensions;
using NewAvalon.Storage.Infrastructure.Options;

namespace NewAvalon.App.ServiceInstallers.Storage
{
    public class StorageServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            InstallOptions(services);

            InstallCore(services);
        }

        private static void InstallOptions(IServiceCollection services) => services.ConfigureOptions<StorageBucketOptionsSetup>();

        private static void InstallCore(IServiceCollection services)
        {
            services.AddTransientServicesAsMatchingInterfaces(typeof(NewAvalon.Storage.Infrastructure.AssemblyReference).Assembly);

            services.AddTransient<IAmazonS3>(factory =>
            {
                StorageBucketOptions imageStorageBucketOptions =
                    factory.GetRequiredService<IOptions<StorageBucketOptions>>().Value;

                var awsCredentials = new BasicAWSCredentials(imageStorageBucketOptions.AccessKey, imageStorageBucketOptions.SecretKey);

                var regionEndpoint = RegionEndpoint.GetBySystemName(imageStorageBucketOptions.Region);

                var amazonS3Client = new AmazonS3Client(awsCredentials, regionEndpoint);

                return amazonS3Client;
            });
        }
    }
}
