using Microsoft.EntityFrameworkCore.Diagnostics;
using SequentialOutbox.Domain.Abstract;
using SequentialOutbox.Infrastructure.Outbox.Entities;
using SequentialOutbox.Infrastructure.Outbox.Factories;

namespace SequentialOutbox.Infrastructure.Outbox.Interceptors;

internal sealed class OutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, 
        int result,
        CancellationToken cancellationToken = new())
    {
        var aggregates = eventData.Context!.ChangeTracker.Entries()
            .Where(e => e.Entity is AggregateRoot)
            .Select(e => (AggregateRoot)e.Entity)
            .ToList();

        var aggregateEvents = aggregates.Select(a => (a, a.GetEvents().ToList())).ToList();

        var outboxMessages = new List<OutboxMessage>();
        
        foreach (var (aggregate, events) in aggregateEvents)
        {
            var stream = aggregate.Id.ToString();

            var messages = events.Select(e => OutboxMessageFactory.CreateMessage(e, stream: stream));
            
            outboxMessages.AddRange(messages);
        }

        if (outboxMessages.Any())
        {
            eventData.Context.Set<OutboxMessage>().AddRange(outboxMessages);
            await eventData.Context.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}