namespace EStore.Outbox.Entities;

internal sealed class InboxMessage
{
    public long Id { get; init; }
    
    public required long MessageId { get; init; }
    
    public required string Topic { get; init; }
    
    public required string Stream { get; init; }
}