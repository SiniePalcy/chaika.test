using Chaika.Domain.Results;
using Chaika.Domain.ValueObjects;
using MediatR;

namespace Chaika.Domain.Messaging;

/// <summary>
/// Command to create a booking. Dispatched through MediatR; intentionally not implemented for this task.
/// </summary>
public sealed record CreateBookingCommand(
    string HotelId,
    string RoomId,
    string RatePlanId,
    DateOnly CheckInDate,
    DateOnly CheckOutDate,
    GuestComposition Guests,
    Customer Customer) : IRequest<CreateBookingResult>;
