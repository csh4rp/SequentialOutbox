using SequentialOutbox.Domain.Abstract;

namespace SequentialOutbox.Application.Services;

public interface IEventBus
{
    Task PublishAsync(IEvent @event, CancellationToken cancellationToken);

    Task PublishAsync(IEvent @event, EventPublishOptions options, CancellationToken cancellationToken);
}