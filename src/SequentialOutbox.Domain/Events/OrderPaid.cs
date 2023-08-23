using SequentialOutbox.Domain.Abstract;
using SequentialOutbox.Domain.Attributes;

namespace SequentialOutbox.Domain.Events;

[Event("Orders")]
public record OrderPaid(long OrderId) : IEvent;