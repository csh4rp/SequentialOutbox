using EStore.Outbox.Abstract;
using EStore.Outbox.Factories;
using EStore.Outbox.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EStore.Outbox;

public static class OutboxExtensions
{
    public static IServiceCollection AddOutbox(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IEventBus, OutboxEventBus>()
            .AddSingleton<OutboxMessageFactory>();
        
        return serviceCollection;
    }
}