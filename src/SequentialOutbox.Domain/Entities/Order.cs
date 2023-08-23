using SequentialOutbox.Domain.Abstract;
using SequentialOutbox.Domain.Enums;
using SequentialOutbox.Domain.Events;

namespace SequentialOutbox.Domain.Entities;

public class Order : AggregateRoot
{
    public Order()
    {
        
    }
    
    public Order(string email, string firstAddressLine, string secondAddressLine, IEnumerable<OrderItem> items)
    {
        Email = email;
        FirstAddressLine = firstAddressLine;
        SecondAddressLine = secondAddressLine;
        Items = items.ToList();
    }

    public long Id { get; init; }
    
    public DateTimeOffset CreatedAt { get; private init; }
    
    public OrderStatus Status { get; private set; }

    public string Email { get; private init; } = default!;

    public string FirstAddressLine { get; private init; } = default!;

    public string SecondAddressLine { get; private init; } = default!;

    public IReadOnlyList<OrderItem> Items { get; private set; } = new List<OrderItem>();

    public void MarkAsPaid()
    {
        if (Status != OrderStatus.New)
        {
            throw new InvalidOperationException("Can't mark order as paid unless it's new");
        }
        
        RegisterEvent(new OrderPaid(Id));
        Status = OrderStatus.Paid;
    }

    public void MarkAsPrepared()
    {
        if (Status != OrderStatus.Paid)
        {
            throw new InvalidOperationException("Order must be paid in order to be marked as prepared");
        }
        
        RegisterEvent(new OrderPrepared(Id));
        Status = OrderStatus.Prepared;
    }

    public void MarkAsSent()
    {
        if (Status != OrderStatus.Prepared)
        {
            throw new InvalidOperationException("Order must be prepared before it can be send");
        }
        
        RegisterEvent(new OrderSent(Id));
        Status = OrderStatus.InDelivery;
    }
    
    public void MarkAsDelivered()
    {
        if (Status != OrderStatus.InDelivery)
        {
            throw new InvalidOperationException("Order must in delivery before it can be delivered");
        }
        
        RegisterEvent(new OrderSent(Id));
        Status = OrderStatus.InDelivery;
    }
    
}