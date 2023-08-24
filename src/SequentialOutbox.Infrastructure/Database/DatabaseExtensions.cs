using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SequentialOutbox.Domain.Repositories;
using SequentialOutbox.Infrastructure.Database.Contexts;
using SequentialOutbox.Infrastructure.Database.Repositories;
using SequentialOutbox.Infrastructure.Outbox.Interceptors;
using Wolverine;
using Wolverine.EntityFrameworkCore;

namespace SequentialOutbox.Infrastructure.Database;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContextWithWolverineIntegration<StoreDbContext>(opt =>
        {
            opt.AddInterceptors(new OutboxMessagesInterceptor());
            opt.UseNpgsql(configuration.GetConnectionString("Postgres"));
        });

        serviceCollection.AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IOrderRepository, OrderRepository>();

        return serviceCollection;
    }
}