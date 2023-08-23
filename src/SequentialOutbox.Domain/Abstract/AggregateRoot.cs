namespace SequentialOutbox.Domain.Abstract;

public abstract class AggregateRoot : IEvent
{
    private readonly Queue<IEvent> _events = new();

    protected void RegisterEvent(IEvent @event) => _events.Enqueue(@event);

    public IEnumerable<IEvent> GetEvents()
    {
        while (_events.TryDequeue(out var @event))
        {
            yield return @event;
        }
    }
}