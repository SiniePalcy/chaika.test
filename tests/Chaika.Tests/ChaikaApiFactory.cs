using Chaika.Application.Abstractions;
using Chaika.Tests.TestDoubles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Chaika.Tests;

/// <summary>
/// Test host that pins the clock to a fixed date so date-based validation is deterministic.
/// </summary>
public sealed class ChaikaApiFactory : WebApplicationFactory<Program>
{
    static readonly DateOnly Today = new(2026, 6, 22);

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<IClock>();
            services.AddSingleton<IClock>(new FixedClock(Today));
        });
    }
}
