namespace SequentialOutbox.Infrastructure.Outbox.Entities;

internal sealed class OutboxMessage
{
    public long Id { get; init; }
    
    public required DateTimeOffset CreatedAt { get; init; }
    
    public DateTimeOffset? PublishedAt { get; init; }
    
    public required string Topic { get; init; }
    
    public required string Stream { get; init; }
    
    public required string PayloadType { get; init; }
    
    public required string Payload { get; init; }
}