using Chaika.Application.Abstractions;
using Chaika.Application.Handlers;
using Chaika.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chaika.Application;

/// <summary>
/// Registers Application-layer services and MediatR handlers.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAvailabilityService, AvailabilityService>();

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblyContaining<SearchAvailabilityQueryHandler>());

        return services;
    }
}
