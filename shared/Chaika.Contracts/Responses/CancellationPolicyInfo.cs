namespace Chaika.Contracts.Responses;

public sealed record CancellationPolicyInfo(
    bool IsRefundable,
    DateTimeOffset? FreeCancellationUntil);
