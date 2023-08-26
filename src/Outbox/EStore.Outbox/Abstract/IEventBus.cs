using EStore.Application.Services;

namespace EStore.Outbox.Abstract;

public interface IEventBus
{
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken);

    Task PublishAsync<T>(T @event, EventPublishOptions options, CancellationToken cancellationToken);
}