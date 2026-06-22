namespace Chaika.Application.Exceptions;

/// <summary>
/// Thrown when a requested resource does not exist. Mapped to 404 Not Found.
/// </summary>
public sealed class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    {
    }
}
