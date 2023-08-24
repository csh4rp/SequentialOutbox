using SequentialOutbox.Domain.Abstract;

namespace SequentialOutbox.Domain.Entities;

public class OrderItem : IEntity
{
    private OrderItem()
    {
    }

    public OrderItem(long productId, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public long Id { get; init; }
    
    public long OrderId { get; init; }
    
    public long ProductId { get; private set; }
    
    public int Quantity { get; private set; }
    
    public decimal UnitPrice { get; private set; }
}