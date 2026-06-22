namespace Chaika.Application.Abstractions;

/// <summary>
/// Abstraction over the system clock so date-dependent logic and validation can be tested deterministically.
/// </summary>
public interface IClock
{
    DateOnly Today { get; }

    DateTimeOffset UtcNow { get; }
}
