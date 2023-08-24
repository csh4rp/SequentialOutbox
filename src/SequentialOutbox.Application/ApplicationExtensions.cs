using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace SequentialOutbox.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddValidatorsFromAssembly(typeof(ApplicationExtensions).Assembly, includeInternalTypes: true);
        
        return serviceCollection;
    }
}