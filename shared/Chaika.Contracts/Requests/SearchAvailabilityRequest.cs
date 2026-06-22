namespace Chaika.Contracts.Requests;

/// <summary>
/// Request body for <c>POST /api/availability/search</c>.
/// </summary>
public sealed record SearchAvailabilityRequest(
    string HotelId,
    DateOnly CheckInDate,
    DateOnly CheckOutDate,
    int RoomsCount,
    int AdultsCount,
    IReadOnlyCollection<int>? ChildrenAges);
