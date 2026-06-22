namespace Chaika.Domain.Entities;

/// <summary>
/// A bookable room type within a hotel, with its available rate plans.
/// </summary>
public sealed class Room
{
    public Room(string id, string name, int maxOccupancy, IReadOnlyCollection<RatePlan> ratePlans)
    {
        Id = id;
        Name = name;
        MaxOccupancy = maxOccupancy;
        RatePlans = ratePlans;
    }

    public string Id { get; }

    public string Name { get; }

    /// <summary>Maximum number of guests (adults + children) a single room can hold.</summary>
    public int MaxOccupancy { get; }

    public IReadOnlyCollection<RatePlan> RatePlans { get; }

    /// <summary>True when a single room can accommodate the requested number of guests per room.</summary>
    public bool CanAccommodate(int guestsPerRoom)
    {
        return MaxOccupancy >= guestsPerRoom;
    }
}
