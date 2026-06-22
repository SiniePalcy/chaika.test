namespace Chaika.Contracts.Responses;

public sealed record RatePlanOption(
    string RatePlanId,
    string Name,
    decimal TotalPrice,
    string Currency,
    string MealPlan,
    CancellationPolicyInfo CancellationPolicy);
