using EStore.Domain.Abstract;

namespace EStore.Domain.Entities;

public class Product : IEntity
{
    public long Id { get; init; }
    
    public required string Name { get; set; }
    
    public required decimal Price { get; set; }
}