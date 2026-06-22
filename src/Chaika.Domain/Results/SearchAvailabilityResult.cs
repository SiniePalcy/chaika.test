using Chaika.Domain.Enums;
using Chaika.Domain.ValueObjects;

namespace Chaika.Domain.Results;

/// <summary>
/// Outcome of an availability search: the hotel, the stay and the rooms that can be booked.
/// </summary>
public sealed record SearchAvailabilityResult(
    string HotelId,
    string HotelName,
    StayPeriod Stay,
    IReadOnlyCollection<AvailableRoomResult> Rooms);

public sealed record AvailableRoomResult(
    string RoomId,
    string Name,
    int MaxOccupancy,
    IReadOnlyCollection<RatePlanOffer> RatePlans);

public sealed record RatePlanOffer(
    string RatePlanId,
    string Name,
    Money TotalPrice,
    MealPlan MealPlan,
    CancellationPolicy CancellationPolicy);
