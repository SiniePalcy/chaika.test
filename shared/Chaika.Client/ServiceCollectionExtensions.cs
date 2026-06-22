using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Chaika.Client;

/// <summary>
/// DI registration for the typed Chaika API client.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddChaikaClient(
        this IServiceCollection services,
        Uri baseAddress)
    {
        services
            .AddRefitClient<IChaikaApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = baseAddress;
            });

        return services;
    }
}
