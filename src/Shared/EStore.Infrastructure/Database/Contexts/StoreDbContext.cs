using Microsoft.EntityFrameworkCore;
using EStore.Domain.Entities;

namespace EStore.Infrastructure.Database.Contexts;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = default!;
    
    public DbSet<OrderItem> OrderItems { get; set; } = default!;
    
    public DbSet<Product> Products { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
    }
}