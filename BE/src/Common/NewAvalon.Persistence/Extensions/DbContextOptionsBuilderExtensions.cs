using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Persistence.Relational.Interceptors;
using System;

namespace NewAvalon.Persistence.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder AddInterceptors(
            this DbContextOptionsBuilder builder,
            IServiceProvider serviceProvider)
        {
            builder.AddInterceptors(
                serviceProvider.GetRequiredService<ConvertDomainEventsToMessagesInterceptor>(),
                serviceProvider.GetRequiredService<UpdateAuditableEntitiesInterceptor>());

            return builder;
        }
    }
}
