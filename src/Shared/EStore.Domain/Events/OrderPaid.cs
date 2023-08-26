using EStore.Domain.Abstract;
using EStore.Domain.Attributes;

namespace EStore.Domain.Events;

[Event("Orders")]
public record OrderPaid(long OrderId) : IEvent;