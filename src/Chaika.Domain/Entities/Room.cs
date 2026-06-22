namespace Chaika.Domain.Entities;

/// <summary>
/// A bookable room type within a hotel, with its available rate plans.
/// </summary>
public sealed class Room(string id, string name, int maxOccupancy, IReadOnlyCollection<RatePlan> ratePlans)
{
    public string Id { get; } = id;

    public string Name { get; } = name;

    /// <summary>Maximum number of guests (adults + children) a single room can hold.</summary>
    public int MaxOccupancy { get; } = maxOccupancy;

    public IReadOnlyCollection<RatePlan> RatePlans { get; } = ratePlans;

    /// <summary>
    /// True when <paramref name="roomsCount"/> rooms of this type can collectively hold
    /// <paramref name="totalGuests"/> guests (i.e. total capacity covers the whole party).
    /// </summary>
    public bool CanAccommodate(int totalGuests, int roomsCount)
    {
        return MaxOccupancy * roomsCount >= totalGuests;
    }
}
