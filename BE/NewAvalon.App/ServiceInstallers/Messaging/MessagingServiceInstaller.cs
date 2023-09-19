using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewAvalon.App.Abstractions;
using NewAvalon.Catalog.Business.Products.Consumers.GetCatalogProductListRequest;
using NewAvalon.Catalog.Business.Products.Consumers.ImageDeletedEvent;
using NewAvalon.Catalog.Business.Products.Consumers.OrderDeletedEvent;
using NewAvalon.Catalog.Business.Products.Consumers.ProductAddedEvent;
using NewAvalon.Infrastructure.Messaging.Options;
using NewAvalon.Messaging.Contracts.Files;
using NewAvalon.Messaging.Contracts.Images;
using NewAvalon.Messaging.Contracts.Permissions;
using NewAvalon.Messaging.Contracts.Products;
using NewAvalon.Messaging.Contracts.Users;
using NewAvalon.Notification.Business.Notifications.Consumers;
using NewAvalon.Order.Business.Orders.Consumers;
using NewAvalon.Storage.Business.Files.Consumers;
using NewAvalon.Storage.Business.Images.Consumers;
using NewAvalon.UserAdministration.Business.Permissions.Consumers;
using NewAvalon.UserAdministration.Business.Users.Consumers.ImageDeletedEvent;
using NewAvalon.UserAdministration.Business.Users.Consumers.UserDetailsListRequest;
using NewAvalon.UserAdministration.Business.Users.Consumers.UserDetailsRequest;

namespace NewAvalon.App.ServiceInstallers.Messaging
{
    public class MessagingServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            InstallOptions(services);

            InstallCore(services);
        }

        private static void InstallOptions(IServiceCollection services) => services.ConfigureOptions<MessageBrokerOptionsSetup>();

        private static void InstallCore(IServiceCollection services)
        {
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                AddConsumers(busConfigurator);

                AddRequestClients(busConfigurator);

                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    MessageBrokerOptions messageBrokerOptions =
                        context.GetRequiredService<IOptions<MessageBrokerOptions>>().Value;

                    //configurator.Host(new Uri(messageBrokerOptions.Host), h =>
                    //{
                    //    h.Username(messageBrokerOptions.Username);
                    //    h.Password(messageBrokerOptions.Password);
                    //});

                    configurator.Host(messageBrokerOptions.Host);

                    configurator.ConfigureEndpoints(context);
                });
            });

            services.AddMassTransitHostedService();
        }

        private static void AddConsumers(IRegistrationConfigurator busConfigurator)
        {
            busConfigurator.AddConsumer<UserDetailsRequestConsumer>().Endpoint(e => e.Name = "user-details-request-consumer");

            busConfigurator.AddConsumer<GetPermissionsRequestConsumer>().Endpoint(e => e.Name = "user-administration-get-permissions-request");

            busConfigurator.AddConsumer<NotificationCreatedEventConsumer>().Endpoint(e => e.Name = "notification-created-event-consumer");

            busConfigurator.AddConsumer<UserDetailsListRequestConsumer>().Endpoint(e => e.Name = "user-details-list-request-consumer");

            busConfigurator.AddConsumer<FileDeletedEventConsumer>().Endpoint(e => e.Name = "file-deleted-consumer");

            busConfigurator.AddConsumer<FIleListDeletedEventConsumer>().Endpoint(e => e.Name = "file-list-deleted-consumer");

            busConfigurator.AddConsumer<ImageRequestConsumer>().Endpoint(e => e.Name = "image-request-consumer");

            busConfigurator.AddConsumer<FileListRequestConsumer>().Endpoint(e => e.Name = "file-list-request-consumer");

            busConfigurator.AddConsumer<StoreUploadedFileRequestConsumer>().Endpoint(e => e.Name = "store-uploaded-file-request-consumer");

            busConfigurator.AddConsumer<UploadFileRequestConsumer>().Endpoint(e => e.Name = "upload-file-request-consumer");

            busConfigurator.AddConsumer<UserProfileImageChangedEventConsumer>().Endpoint(e => e.Name = "storage-user-profile-image-changed");

            busConfigurator.AddConsumer<ProductImageChangedEventConsumer>().Endpoint(e => e.Name = "storage-product-image-changed");

            busConfigurator.AddConsumer<ProductImageChangedEventConsumer>().Endpoint(e => e.Name = "storage-product-image-changed");

            busConfigurator.AddConsumer<ProfileImageDeletedEventConsumer>().Endpoint(e => e.Name = "profile-image-deleted-event-consumer");

            busConfigurator.AddConsumer<ProductImageDeletedEventConsumer>().Endpoint(e => e.Name = "product-image-deleted-event-consumer");

            busConfigurator.AddConsumer<ProductAddedEventConsumer>().Endpoint(e => e.Name = "product-added-event-consumer");

            busConfigurator.AddConsumer<GetCatalogProductListRequestConsumer>().Endpoint(e => e.Name = "get-catalog-product-list-request-consumer");

            busConfigurator.AddConsumer<IsProductUsedRequestConsumer>().Endpoint(e => e.Name = "is-product-used-request-consumer");

            busConfigurator.AddConsumer<OrderDeletedEventConsumer>().Endpoint(e => e.Name = "order-deleted-event-consumer");
        }

        private static void AddRequestClients(IRegistrationConfigurator busConfigurator)
        {
            busConfigurator.AddRequestClient<IUserDetailsRequest>();

            busConfigurator.AddRequestClient<IGetPermissionsRequest>();

            busConfigurator.AddRequestClient<IUserDetailsListRequest>();

            busConfigurator.AddRequestClient<IImageRequest>();

            busConfigurator.AddRequestClient<IFileListRequest>();

            busConfigurator.AddRequestClient<IUploadFileRequest>();

            busConfigurator.AddRequestClient<IStoreUploadedFileRequest>();

            busConfigurator.AddRequestClient<IGetCatalogProductListRequest>();

            busConfigurator.AddRequestClient<IIsProductUsedRequest>();
        }
    }
}
