using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NewAvalon.Domain.Abstractions;
using NewAvalon.Persistence.Relational.Outbox;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Persistence.Relational.Interceptors
{
    public sealed class ConvertDomainEventsToMessagesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            ConvertDomainEventsToMessages(eventData.Context);

            return new ValueTask<InterceptionResult<int>>(result);
        }

        private static void ConvertDomainEventsToMessages(DbContext dbContext)
        {
            var aggregateRoots = dbContext.ChangeTracker
                .Entries<IAggregateRoot>()
                .Select(entityEntry => entityEntry.Entity)
                .Where(aggregateRoot => aggregateRoot.DomainEvents.Any())
                .ToList();

            foreach (IAggregateRoot aggregateRoot in aggregateRoots)
            {
                foreach (IDomainEvent domainEvent in aggregateRoot.DomainEvents)
                {
                    string content = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                    var message = new Message(Guid.NewGuid(), content);

                    dbContext.Set<Message>().Add(message);
                }

                aggregateRoot.ClearDomainEvents();
            }
        }
    }
}
