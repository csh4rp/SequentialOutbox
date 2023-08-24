namespace SequentialOutbox.Infrastructure.Outbox.Entities;

public class OutboxMessageGeneration
{
    public long Id { get; init; }
    
    public DateTimeOffset StartsAt { get; init; }
    
    public DateTimeOffset EndsAt { get; init; }
}