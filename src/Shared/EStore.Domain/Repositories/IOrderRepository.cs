using EStore.Domain.Entities;

namespace EStore.Domain.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken);

    Task UpdateAsync(Order order, CancellationToken cancellationToken);

    Task<Order?> FindAsync(long orderId, CancellationToken cancellationToken);
}