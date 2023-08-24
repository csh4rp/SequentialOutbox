using SequentialOutbox.Infrastructure.Outbox.Entities;

namespace SequentialOutbox.Infrastructure.Outbox.Models;

internal sealed class OutboxMessageWrapper
{
    public required OutboxMessage Message { get; init; }
    
    public required long SequenceNumber { get; init; }
    
    public required long Generation { get; init; }
    
    public required long PrevGenLastSequenceNumber { get; init; }
}