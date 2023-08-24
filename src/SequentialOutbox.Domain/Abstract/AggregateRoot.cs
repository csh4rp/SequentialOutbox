namespace SequentialOutbox.Domain.Abstract;

public abstract class AggregateRoot : IEntity
{
    private readonly Queue<IEvent> _events = new();

    public long Id { get; init; }
    
    protected void RegisterEvent(IEvent @event) => _events.Enqueue(@event);

    public IEnumerable<IEvent> GetEvents()
    {
        while (_events.TryDequeue(out var @event))
        {
            yield return @event;
        }
    }

}