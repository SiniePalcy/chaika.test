namespace Chaika.Application.Exceptions;

/// <summary>
/// Thrown for endpoints that are intentionally not implemented. Mapped to 501 Not Implemented.
/// </summary>
public sealed class NotImplementedFeatureException : Exception
{
    public NotImplementedFeatureException(string message)
        : base(message)
    {
    }
}
