using Chaika.Application.Abstractions;

namespace Chaika.Tests.TestDoubles;

/// <summary>
/// Deterministic <see cref="IClock"/> for tests.
/// </summary>
public sealed class FixedClock(DateOnly today) : IClock
{
    public DateOnly Today { get; } = today;

    public DateTimeOffset UtcNow { get; } = new(today.ToDateTime(TimeOnly.MinValue), TimeSpan.Zero);
}
