using SequentialOutbox.Domain.Abstract;

namespace SequentialOutbox.Domain.Entities;

public class Product : IEntity
{
    public long Id { get; init; }
    
    public required string Name { get; set; }
    
    public required decimal Price { get; set; }
}