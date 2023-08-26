using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EStore.Domain.Repositories;
using EStore.Infrastructure.Database.Contexts;
using EStore.Infrastructure.Database.Repositories;
using Wolverine.EntityFrameworkCore;

namespace EStore.Infrastructure.Database;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContextWithWolverineIntegration<StoreDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });

        serviceCollection.AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IOrderRepository, OrderRepository>();

        return serviceCollection;
    }
}