using Microsoft.Extensions.DependencyInjection;
using SequentialOutbox.Application.Services;
using SequentialOutbox.Infrastructure.Outbox.Services;

namespace SequentialOutbox.Infrastructure.Outbox;

public static class OutboxExtensions
{
    public static IServiceCollection AddOutbox(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IEventBus, OutboxEventBus>();
        
        return serviceCollection;
    }
}