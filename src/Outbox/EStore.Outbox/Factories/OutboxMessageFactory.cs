using System.Reflection;
using System.Text.Json;
using EStore.Outbox.Attributes;
using EStore.Outbox.Entities;

namespace EStore.Outbox.Factories;

internal sealed class OutboxMessageFactory
{
    private const string DefaultTopic = "*DEFAULT*";
    private const string DefaultStream = "*DEFAULT*";
    
    public static OutboxMessage CreateMessage<T>(T @event, string? topic = null, string? stream = null)
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
            Payload = JsonSerializer.Serialize(@event, eventType),
            PayloadType = eventType.FullName!,
            Topic = topic,
            Stream = stream,
        };
    }
}