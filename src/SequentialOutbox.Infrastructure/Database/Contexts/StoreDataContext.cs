using Microsoft.EntityFrameworkCore;
using SequentialOutbox.Domain.Entities;

namespace SequentialOutbox.Infrastructure.Database.Contexts;

public class StoreDataContext : DbContext
{
    public DbSet<Order> Orders { get; set; } = default!;
    
    public DbSet<OrderItem> OrderItems { get; set; } = default!;
    
    public DbSet<Product> Products { get; set; } = default!;
    
}