using Microsoft.EntityFrameworkCore;
using SequentialOutbox.Domain.Entities;
using SequentialOutbox.Domain.Repositories;
using SequentialOutbox.Infrastructure.Database.Contexts;

namespace SequentialOutbox.Infrastructure.Database.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly StoreDbContext _storeDbContext;

    public OrderRepository(StoreDbContext storeDbContext) => _storeDbContext = storeDbContext;

    public Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        _storeDbContext.Orders.Add(order);
        return _storeDbContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        _storeDbContext.Orders.Update(order);
        return _storeDbContext.SaveChangesAsync(cancellationToken);

    }

    public Task<Order?> FindAsync(long orderId, CancellationToken cancellationToken) => 
        _storeDbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
}