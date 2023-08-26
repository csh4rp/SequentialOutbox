using EStore.Outbox.Entities;

namespace EStore.Outbox.Models;

internal sealed class OutboxMessageWrapper
{
    public required OutboxMessage Message { get; init; }
    
    public required long SequenceNumber { get; init; }
    
    public required long Generation { get; init; }
    
    public required long PrevGenLastSequenceNumber { get; init; }
}