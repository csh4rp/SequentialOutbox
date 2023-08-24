using System.Reflection;
using System.Text.Json;
using SequentialOutbox.Domain.Abstract;
using SequentialOutbox.Domain.Attributes;
using SequentialOutbox.Infrastructure.Outbox.Entities;

namespace SequentialOutbox.Infrastructure.Outbox.Factories;

internal sealed class OutboxMessageFactory
{
    private const string DefaultTopic = "*DEFAULT*";
    private const string DefaultStream = "*DEFAULT*";
    
    public static OutboxMessage CreateMessage(IEvent @event, string? topic = null, string? stream = null)
    {
        var eventType = @event.GetType();

        if (string.IsNullOrEmpty(topic))
        {
            topic = eventType.GetCustomAttribute<EventAttribute>()?.Topic ?? DefaultTopic;
        }

        if (string.IsNullOrEmpty(stream))
        {
            stream = DefaultStream;
        }

        return new OutboxMessage
        {
            CreatedAt = DateTimeOffset.UtcNow,
            Payload = JsonSerializer.Serialize(@event),
            PayloadType = eventType.FullName!,
            Topic = topic,
            Stream = stream,
        };
    }
}