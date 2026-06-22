namespace Chaika.Domain.Results;

/// <summary>
/// Result of a booking creation. Booking is intentionally not implemented for this task.
/// </summary>
public sealed record CreateBookingResult(string BookingId, string Status);
