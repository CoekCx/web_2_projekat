using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewAvalon.Abstractions.Clock;
using NewAvalon.Order.Persistence.Options;
using NewAvalon.Persistence.BackgroundTasks;

namespace NewAvalon.Order.Persistence.BackgroundTasks
{
    public sealed class OrderPublishDomainEventsJob
        : PublishDomainEventsJob
    {
        public OrderPublishDomainEventsJob(
            OrderDbContext dbContext,
            IPublisher publisher,
            IOptions<OrderPublishDomainEventsJobOptions> options,
            ISystemTime systemTime,
            ILogger<PublishDomainEventsJob> logger)
            : base(dbContext, publisher, options.Value, systemTime, logger)
        {
        }
    }
}
