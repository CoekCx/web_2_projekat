using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewAvalon.Abstractions.Clock;
using NewAvalon.Persistence.BackgroundTasks;
using NewAvalon.UserAdministration.Persistence.Options;

namespace NewAvalon.UserAdministration.Persistence.BackgroundTasks
{
    public sealed class UserAdministrationPublishDomainEventsJob
        : PublishDomainEventsJob
    {
        public UserAdministrationPublishDomainEventsJob(
            UserAdministrationDbContext dbContext,
            IPublisher publisher,
            IOptions<UserAdministrationPublishDomainEventsJobOptions> options,
            ISystemTime systemTime,
            ILogger<PublishDomainEventsJob> logger) : base(dbContext, publisher, options.Value, systemTime, logger)
        {
        }
    }
}
