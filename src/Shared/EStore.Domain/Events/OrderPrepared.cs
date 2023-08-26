using EStore.Domain.Abstract;
using EStore.Domain.Attributes;

namespace EStore.Domain.Events;

[Event("Orders")]
public record OrderPrepared(long OrderId) : IEvent;