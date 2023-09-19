using MassTransit;
using NewAvalon.Abstractions.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Infrastructure.Messaging
{
    internal sealed class EventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventPublisher(IPublishEndpoint publishEndpoint) => _publishEndpoint = publishEndpoint;

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : class, IEvent
            => await _publishEndpoint.Publish(@event, cancellationToken);
    }
}
