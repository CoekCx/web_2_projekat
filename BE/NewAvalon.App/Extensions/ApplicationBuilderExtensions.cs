using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.App.Middlewares;
using NewAvalon.Catalog.Persistence;
using NewAvalon.Notification.Persistence;
using NewAvalon.Order.Persistence;
using NewAvalon.Storage.Persistence;
using NewAvalon.UserAdministration.Persistence;

namespace NewAvalon.App.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        internal static void UseGlobalExceptionHandler(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ExceptionHandlerMiddleware>();

        internal static void ApplyMigrations(this IApplicationBuilder builder)
        {
            using IServiceScope scope = builder.ApplicationServices.CreateScope();

            ApplyUserAdministrationMigrations(scope);
            ApplyCatalogMigrations(scope);
            ApplyOrderMigrations(scope);
            ApplyNotificationMigrations(scope);
            ApplyStorageMigrations(scope);
        }

        private static void ApplyUserAdministrationMigrations(IServiceScope scope)
        {
            using UserAdministrationDbContext userAdministrationDbContext =
                scope.ServiceProvider.GetRequiredService<UserAdministrationDbContext>();

            userAdministrationDbContext.Database.Migrate();
        }

        private static void ApplyCatalogMigrations(IServiceScope scope)
        {
            using CatalogDbContext catalogDbContext =
                scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

            catalogDbContext.Database.Migrate();
        }

        private static void ApplyOrderMigrations(IServiceScope scope)
        {
            using OrderDbContext orderDbContext =
                scope.ServiceProvider.GetRequiredService<OrderDbContext>();

            orderDbContext.Database.Migrate();
        }

        private static void ApplyNotificationMigrations(IServiceScope scope)
        {
            using NotificationDbContext notificationDbContext =
                scope.ServiceProvider.GetRequiredService<NotificationDbContext>();

            notificationDbContext.Database.Migrate();
        }

        private static void ApplyStorageMigrations(IServiceScope scope)
        {
            using StorageDbContext notificationDbContext =
                scope.ServiceProvider.GetRequiredService<StorageDbContext>();

            notificationDbContext.Database.Migrate();
        }
    }
}
