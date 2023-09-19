using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewAvalon.Abstractions.Clock;
using NewAvalon.Catalog.Persistence.Options;
using NewAvalon.Persistence.BackgroundTasks;

namespace NewAvalon.Catalog.Persistence.BackgroundTasks
{
    public sealed class CatalogPublishDomainEventsJob
        : PublishDomainEventsJob
    {
        public CatalogPublishDomainEventsJob(
            CatalogDbContext dbContext,
            IPublisher publisher,
            IOptions<CatalogPublishDomainEventsJobOptions> options,
            ISystemTime systemTime,
            ILogger<PublishDomainEventsJob> logger)
            : base(dbContext, publisher, options.Value, systemTime, logger)
        {
        }
    }
}
