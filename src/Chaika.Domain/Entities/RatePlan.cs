using Chaika.Domain.Enums;
using Chaika.Domain.ValueObjects;

namespace Chaika.Domain.Entities;

/// <summary>
/// A priced offer for a room: nightly price, meal plan and cancellation policy.
/// </summary>
public sealed class RatePlan(
    string id,
    string name,
    Money pricePerNight,
    MealPlan mealPlan,
    CancellationPolicy cancellationPolicy)
{
    public string Id { get; } = id;

    public string Name { get; } = name;

    public Money PricePerNight { get; } = pricePerNight;

    public MealPlan MealPlan { get; } = mealPlan;

    public CancellationPolicy CancellationPolicy { get; } = cancellationPolicy;

    /// <summary>Total price for the whole stay across all requested rooms.</summary>
    public Money TotalPrice(int nights, int roomsCount)
    {
        return PricePerNight.Multiply(nights * roomsCount);
    }
}
