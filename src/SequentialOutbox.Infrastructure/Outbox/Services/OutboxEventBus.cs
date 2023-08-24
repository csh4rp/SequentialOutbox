using SequentialOutbox.Application.Services;
using SequentialOutbox.Domain.Abstract;
using SequentialOutbox.Infrastructure.Database.Contexts;
using SequentialOutbox.Infrastructure.Outbox.Entities;
using SequentialOutbox.Infrastructure.Outbox.Factories;

namespace SequentialOutbox.Infrastructure.Outbox.Services;

internal sealed class OutboxEventBus : IEventBus
{
    private readonly StoreDataContext _storeDataContext;

    public OutboxEventBus(StoreDataContext storeDataContext) => _storeDataContext = storeDataContext;

    public Task PublishAsync(IEvent @event, CancellationToken cancellationToken)
    {
        var message = OutboxMessageFactory.CreateMessage(@event);

        _storeDataContext.Set<OutboxMessage>().Add(message);
        return _storeDataContext.SaveChangesAsync(cancellationToken);
    }

    public Task PublishAsync(IEvent @event, EventPublishOptions options, CancellationToken cancellationToken)
    {
        var message = OutboxMessageFactory.CreateMessage(@event, options.Topic, options.Stream);

        _storeDataContext.Set<OutboxMessage>().Add(message);
        return _storeDataContext.SaveChangesAsync(cancellationToken);
    }
}