namespace Chaika.Domain.ValueObjects;

/// <summary>
/// Monetary amount in a given ISO currency. Uses <see cref="decimal"/> for the amount.
/// </summary>
public readonly record struct Money(decimal Amount, string Currency)
{
    public Money Multiply(int factor)
    {
        if (factor < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), factor, "Factor must not be negative.");
        }

        return this with { Amount = Amount * factor };
    }
}
