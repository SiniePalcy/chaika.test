using Chaika.Domain.Enums;
using Chaika.Domain.ValueObjects;

namespace Chaika.Domain.Entities;

/// <summary>
/// A priced offer for a room: nightly price, meal plan and cancellation policy.
/// </summary>
public sealed class RatePlan
{
    public RatePlan(
        string id,
        string name,
        Money pricePerNight,
        MealPlan mealPlan,
        CancellationPolicy cancellationPolicy)
    {
        Id = id;
        Name = name;
        PricePerNight = pricePerNight;
        MealPlan = mealPlan;
        CancellationPolicy = cancellationPolicy;
    }

    public string Id { get; }

    public string Name { get; }

    public Money PricePerNight { get; }

    public MealPlan MealPlan { get; }

    public CancellationPolicy CancellationPolicy { get; }

    /// <summary>Total price for the whole stay across all requested rooms.</summary>
    public Money TotalPrice(int nights, int roomsCount)
    {
        return PricePerNight.Multiply(nights * roomsCount);
    }
}
