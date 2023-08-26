namespace EStore.Outbox.Attributes;

public class EventAttribute : Attribute
{
    public EventAttribute(string topic)
    {
        Topic = topic;
    }

    public string Topic { get; }
}