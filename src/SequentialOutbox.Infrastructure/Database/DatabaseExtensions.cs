using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SequentialOutbox.Infrastructure.Database.Contexts;
using SequentialOutbox.Infrastructure.Outbox.Interceptors;

namespace SequentialOutbox.Infrastructure.Database;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<StoreDataContext>(opt =>
        {
            opt.AddInterceptors(new OutboxMessagesInterceptor());
            opt.UseNpgsql(configuration.GetConnectionString("Postgres"));
        });

        return serviceCollection;
    }
}