namespace Chaika.Domain.ValueObjects;

/// <summary>
/// Number of rooms, adults and children (with their ages) requested for a stay.
/// Children ages are always normalized to a non-null collection.
/// </summary>
public sealed record GuestComposition
{
    public GuestComposition(int roomsCount, int adultsCount, IReadOnlyCollection<int>? childrenAges)
    {
        RoomsCount = roomsCount;
        AdultsCount = adultsCount;
        ChildrenAges = childrenAges ?? Array.Empty<int>();
    }

    public int RoomsCount { get; }

    public int AdultsCount { get; }

    public IReadOnlyCollection<int> ChildrenAges { get; }

    public int ChildrenCount => ChildrenAges.Count;

    /// <summary>Total number of guests across all requested rooms.</summary>
    public int TotalGuests => AdultsCount + ChildrenCount;
}
