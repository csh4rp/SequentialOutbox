using EStore.Domain.Abstract;
using EStore.Domain.Attributes;
namespace EStore.Domain.Events;

[Event("Orders")]
public record OrderDelivered(long OrderId) : IEvent;