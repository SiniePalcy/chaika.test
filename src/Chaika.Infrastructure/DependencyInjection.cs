using Chaika.Application.Abstractions;
using Chaika.Infrastructure.Repositories;
using Chaika.Infrastructure.Time;
using Microsoft.Extensions.DependencyInjection;

namespace Chaika.Infrastructure;

/// <summary>
/// Registers Infrastructure-layer services (mock repository and system clock).
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IHotelRepository, MockHotelRepository>();
        services.AddSingleton<IClock, SystemClock>();

        return services;
    }
}
