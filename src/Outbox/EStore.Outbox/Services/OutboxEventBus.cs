using EStore.Application.Services;
using EStore.Outbox.Abstract;
using EStore.Outbox.Entities;
using EStore.Outbox.Factories;
using Microsoft.EntityFrameworkCore;

namespace EStore.Outbox.Services;

internal sealed class OutboxEventBus : IEventBus
{
    private readonly DbContext _dbContext;

    public OutboxEventBus(DbContext dbContext) => _dbContext = dbContext;

    public Task PublishAsync<T>(T @event, CancellationToken cancellationToken)
    {
        var message = OutboxMessageFactory.CreateMessage(@event);

        _dbContext.Set<OutboxMessage>().Add(message);
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task PublishAsync<T>(T @event, EventPublishOptions options, CancellationToken cancellationToken)
    {
        var message = OutboxMessageFactory.CreateMessage(@event, options.Topic, options.Stream);

        _dbContext.Set<OutboxMessage>().Add(message);
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}