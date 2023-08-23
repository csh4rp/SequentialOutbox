using SequentialOutbox.Domain.Entities;
using SequentialOutbox.Domain.Repositories;
using SequentialOutbox.Infrastructure.Database.Contexts;

namespace SequentialOutbox.Infrastructure.Database.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly DataContext _dataContext;

    public ProductRepository(DataContext dataContext) => _dataContext = dataContext;

    public Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        _dataContext.Add(product);
        return _dataContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _dataContext.Update(product);
        return _dataContext.SaveChangesAsync(cancellationToken);
    }
}