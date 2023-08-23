using SequentialOutbox.Domain.Abstract;

namespace SequentialOutbox.Domain.Entities;

public class OrderItem : IEntity
{
    public long Id { get; init; }
    
    public long OrderId { get; init; }
    
    public required long ProductId { get; init; }
    
    public required int Quantity { get; init; }
    
    public decimal UnitPrice { get; init; }
}