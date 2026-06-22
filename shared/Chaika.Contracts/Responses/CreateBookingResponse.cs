namespace Chaika.Contracts.Responses;

/// <summary>
/// Response body for <c>POST /api/bookings</c>.
/// </summary>
public sealed record CreateBookingResponse(
    string BookingId,
    string Status);
