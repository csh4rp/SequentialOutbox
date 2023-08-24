using SequentialOutbox.Domain.Entities;
using SequentialOutbox.Domain.Repositories;
using SequentialOutbox.Infrastructure.Database.Contexts;

namespace SequentialOutbox.Infrastructure.Database.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly StoreDbContext _storeDbContext;

    public ProductRepository(StoreDbContext storeDbContext) => _storeDbContext = storeDbContext;

    public Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        _storeDbContext.Add(product);
        return _storeDbContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _storeDbContext.Update(product);
        return _storeDbContext.SaveChangesAsync(cancellationToken);
    }
}