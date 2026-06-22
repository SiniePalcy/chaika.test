namespace Chaika.Domain.ValueObjects;

/// <summary>
/// Describes whether a rate plan is refundable and, if so, until when free cancellation is allowed.
/// </summary>
public sealed record CancellationPolicy(bool IsRefundable, DateTimeOffset? FreeCancellationUntil)
{
    public static CancellationPolicy NonRefundable { get; } = new(false, null);

    public static CancellationPolicy RefundableUntil(DateTimeOffset deadline)
    {
        return new CancellationPolicy(true, deadline);
    }
}
