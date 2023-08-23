using SequentialOutbox.Domain.Abstract;
using SequentialOutbox.Domain.Attributes;

namespace SequentialOutbox.Domain.Events;

[Event("Orders")]
public record OrderSent(long OrderId) : IEvent;