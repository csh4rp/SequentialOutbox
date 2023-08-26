using Microsoft.EntityFrameworkCore;
using EStore.Domain.Entities;
using EStore.Domain.Repositories;
using EStore.Infrastructure.Database.Contexts;

namespace EStore.Infrastructure.Database.Repositories;

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