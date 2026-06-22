namespace Chaika.Contracts.Responses;

/// <summary>
/// Response body for <c>POST /api/availability/search</c>.
/// </summary>
public sealed record SearchAvailabilityResponse(
    string HotelId,
    string HotelName,
    DateOnly CheckInDate,
    DateOnly CheckOutDate,
    int Nights,
    IReadOnlyCollection<AvailableRoom> Rooms);
