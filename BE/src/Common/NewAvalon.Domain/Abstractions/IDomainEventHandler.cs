using MediatR;

namespace NewAvalon.Domain.Abstractions
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IDomainEvent
    {
    }
}
