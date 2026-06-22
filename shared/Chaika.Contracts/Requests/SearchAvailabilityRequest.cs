namespace Chaika.Contracts.Requests;

/// <summary>
/// Query parameters for <c>GET /api/availability/search</c>.
/// <para>
/// <see cref="AdultsCount"/> and <see cref="ChildrenAges"/> describe the whole party (totals, not per room).
/// A room type is offered when <see cref="RoomsCount"/> rooms of that type can collectively hold the whole party,
/// and the returned price covers all <see cref="RoomsCount"/> rooms.
/// </para>
/// </summary>
public sealed record SearchAvailabilityRequest(
    string HotelId,
    DateOnly CheckInDate,
    DateOnly CheckOutDate,
    int RoomsCount,
    int AdultsCount,
    IReadOnlyCollection<int>? ChildrenAges);
