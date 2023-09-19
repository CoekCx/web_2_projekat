using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewAvalon.Abstractions.Data;
using NewAvalon.App.Abstractions;
using NewAvalon.Catalog.Domain.Repositories;
using NewAvalon.Catalog.Persistence;
using NewAvalon.Catalog.Persistence.Options;
using NewAvalon.Notification.Domain.Repositories;
using NewAvalon.Notification.Persistence;
using NewAvalon.Notification.Persistence.Options;
using NewAvalon.Order.Domain.Repositories;
using NewAvalon.Order.Persistence;
using NewAvalon.Order.Persistence.Options;
using NewAvalon.Persistence.Extensions;
using NewAvalon.Persistence.Factories;
using NewAvalon.Persistence.Relational.Interceptors;
using NewAvalon.Storage.Domain.Repositories;
using NewAvalon.Storage.Persistence;
using NewAvalon.Storage.Persistence.Options;
using NewAvalon.UserAdministration.Domain.Repositories;
using NewAvalon.UserAdministration.Persistence;
using NewAvalon.UserAdministration.Persistence.Options;
using Scrutor;
using System.Reflection;

namespace NewAvalon.App.ServiceInstallers.Persistence
{
    public class PersistenceServiceInstaller : IServiceInstaller
    {
        private const string RepositoryPostfix = "Repository";
        private const string DataRequestPostfix = "DataRequest";

        public void InstallServices(IServiceCollection services)
        {
            InstallOptions(services);

            InstallCore(services);
        }

        private static void InstallOptions(IServiceCollection services)
        {
            services.ConfigureOptions<UserAdministrationDatabaseOptionsSetup>();
            services.ConfigureOptions<CatalogDatabaseOptionsSetup>();
            services.ConfigureOptions<OrderDatabaseOptionsSetup>();
            services.ConfigureOptions<NotificationDatabaseOptionsSetup>();
            services.ConfigureOptions<StorageDatabaseOptionsSetup>();
        }

        private static void InstallCore(IServiceCollection services)
        {
            AddUserAdministrationDbContext(services);

            AddCatalogDbContext(services);

            AddOrderDbContext(services);

            AddNotificationDbContext(services);

            AddStorageDbContext(services);

            AddRepositories(
                services,
                new[]
                {
                    typeof(UserAdministration.Persistence.AssemblyReference).Assembly,
                    typeof(Catalog.Persistence.AssemblyReference).Assembly,
                    typeof(Order.Persistence.AssemblyReference).Assembly,
                    typeof(Notification.Persistence.AssemblyReference).Assembly,
                    typeof(NewAvalon.Storage.Persistence.AssemblyReference).Assembly,
                });

            AddDataRequests(
                services,
                new[]
                {
                    typeof(UserAdministration.Persistence.AssemblyReference).Assembly,
                    typeof(Catalog.Persistence.AssemblyReference).Assembly,
                    typeof(Order.Persistence.AssemblyReference).Assembly,
                    typeof(Notification.Persistence.AssemblyReference).Assembly,
                    typeof(NewAvalon.Storage.Persistence.AssemblyReference).Assembly,
                });

            AddInterceptors(services);

            services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();
        }

        private static void AddInterceptors(IServiceCollection services)
        {
            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

            services.AddSingleton<ConvertDomainEventsToMessagesInterceptor>();
        }

        private static void AddUserAdministrationDbContext(IServiceCollection services)
        {
            services.AddDbContextPool<UserAdministrationDbContext>((provider, builder) =>
            {
                IOptions<UserAdministrationDatabaseOptions> dbSettingsOptions =
                    provider.GetRequiredService<IOptions<UserAdministrationDatabaseOptions>>();

                builder.UseNpgsql(dbSettingsOptions.Value.GetConnectionString(),
                        optionsBuilder => optionsBuilder.MigrationsAssembly(
                            typeof(UserAdministration.Persistence.AssemblyReference).Assembly.FullName))
                        .AddInterceptors(provider);
            });

            services.AddScoped<IUserAdministrationUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<UserAdministrationDbContext>());
        }

        private static void AddCatalogDbContext(IServiceCollection services)
        {
            services.AddDbContextPool<CatalogDbContext>((provider, builder) =>
            {
                IOptions<CatalogDatabaseOptions> dbSettingsOptions =
                    provider.GetRequiredService<IOptions<CatalogDatabaseOptions>>();

                builder.UseNpgsql(dbSettingsOptions.Value.GetConnectionString(),
                        optionsBuilder => optionsBuilder.MigrationsAssembly(
                            typeof(Catalog.Persistence.AssemblyReference).Assembly.FullName))
                    .AddInterceptors(provider);
            });

            services.AddScoped<ICatalogUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<CatalogDbContext>());
        }

        private static void AddOrderDbContext(IServiceCollection services)
        {
            services.AddDbContextPool<OrderDbContext>((provider, builder) =>
            {
                IOptions<OrderDatabaseOptions> dbSettingsOptions =
                    provider.GetRequiredService<IOptions<OrderDatabaseOptions>>();

                builder.UseNpgsql(dbSettingsOptions.Value.GetConnectionString(),
                        optionsBuilder => optionsBuilder.MigrationsAssembly(
                            typeof(Order.Persistence.AssemblyReference).Assembly.FullName))
                    .AddInterceptors(provider);
            });

            services.AddScoped<IOrderUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<OrderDbContext>());
        }

        private static void AddNotificationDbContext(IServiceCollection services)
        {
            services.AddDbContextPool<NotificationDbContext>((provider, builder) =>
            {
                IOptions<NotificationDatabaseOptions> dbSettingsOptions =
                    provider.GetRequiredService<IOptions<NotificationDatabaseOptions>>();

                builder.UseNpgsql(dbSettingsOptions.Value.GetConnectionString(),
                        optionsBuilder => optionsBuilder.MigrationsAssembly(
                            typeof(Notification.Persistence.AssemblyReference).Assembly.FullName))
                    .AddInterceptors(provider);
            });

            services.AddScoped<INotificationUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<NotificationDbContext>());
        }

        private static void AddStorageDbContext(IServiceCollection services)
        {
            services.AddDbContextPool<StorageDbContext>((provider, builder) =>
            {
                IOptions<StorageDatabaseOptions> dbSettingsOptions =
                    provider.GetRequiredService<IOptions<StorageDatabaseOptions>>();

                builder.UseNpgsql(dbSettingsOptions.Value.GetConnectionString(),
                        optionsBuilder => optionsBuilder.MigrationsAssembly(
                            typeof(NewAvalon.Storage.Persistence.AssemblyReference).Assembly.FullName))
                    .AddInterceptors(provider);
            });

            services.AddScoped<IStorageUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<StorageDbContext>());
        }

        private static void AddRepositories(IServiceCollection services, Assembly[] assemblies) =>
            services.Scan(scan =>
                scan.FromAssemblies(assemblies)
                    .AddClasses(filter => filter.Where(x => x.Name.EndsWith(RepositoryPostfix)), false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                    .AsMatchingInterface()
                    .WithScopedLifetime());

        private static void AddDataRequests(IServiceCollection services, Assembly[] assemblies) =>
            services.Scan(scan =>
                scan.FromAssemblies(assemblies)
                    .AddClasses(filter => filter.Where(x => x.Name.EndsWith(DataRequestPostfix)), false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Append)
                    .AsMatchingInterface()
                    .WithScopedLifetime());
    }
}
