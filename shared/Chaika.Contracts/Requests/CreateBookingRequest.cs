namespace Chaika.Contracts.Requests;

/// <summary>
/// Request body for <c>POST /api/bookings</c>.
/// </summary>
public sealed record CreateBookingRequest(
    string HotelId,
    string RoomId,
    string RatePlanId,
    DateOnly CheckInDate,
    DateOnly CheckOutDate,
    int RoomsCount,
    int AdultsCount,
    IReadOnlyCollection<int>? ChildrenAges,
    CustomerInfo Customer);
