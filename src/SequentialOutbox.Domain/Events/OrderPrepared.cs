using SequentialOutbox.Domain.Abstract;
using SequentialOutbox.Domain.Attributes;

namespace SequentialOutbox.Domain.Events;

[Event("Orders")]
public record OrderPrepared(long OrderId) : IEvent;