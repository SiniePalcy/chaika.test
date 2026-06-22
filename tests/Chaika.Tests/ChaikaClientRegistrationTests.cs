using Chaika.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Chaika.Tests;

public sealed class ChaikaClientRegistrationTests
{
    [Fact]
    public void Refit_client_can_be_registered_and_resolved()
    {
        var services = new ServiceCollection();
        services.AddChaikaClient(new Uri("https://localhost:5001"));

        using var provider = services.BuildServiceProvider();
        var api = provider.GetRequiredService<IChaikaApi>();

        Assert.NotNull(api);
    }
}
