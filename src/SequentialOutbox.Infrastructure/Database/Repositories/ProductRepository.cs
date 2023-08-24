using SequentialOutbox.Domain.Entities;
using SequentialOutbox.Domain.Repositories;
using SequentialOutbox.Infrastructure.Database.Contexts;

namespace SequentialOutbox.Infrastructure.Database.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly StoreDataContext _storeDataContext;

    public ProductRepository(StoreDataContext storeDataContext) => _storeDataContext = storeDataContext;

    public Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        _storeDataContext.Add(product);
        return _storeDataContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _storeDataContext.Update(product);
        return _storeDataContext.SaveChangesAsync(cancellationToken);
    }
}