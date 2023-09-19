using NewAvalon.Abstractions.ServiceLifetimes;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Abstractions.Messaging
{
    public interface IEventPublisher : IScoped
    {
        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : class, IEvent;
    }
}
