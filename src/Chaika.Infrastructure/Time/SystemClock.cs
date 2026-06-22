using Chaika.Application.Abstractions;

namespace Chaika.Infrastructure.Time;

/// <summary>
/// Default <see cref="IClock"/> backed by the real system clock (UTC).
/// </summary>
public sealed class SystemClock : IClock
{
    public DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);

    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
