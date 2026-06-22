using Chaika.Domain.Results;
using MediatR;

namespace Chaika.Domain.Messaging;

/// <summary>
/// Query for available rooms in a hotel for a given stay and guest composition.
/// Dispatched through MediatR and handled in the Application layer.
/// </summary>
public sealed record SearchAvailabilityQuery(
    string HotelId,
    DateOnly CheckInDate,
    DateOnly CheckOutDate,
    int RoomsCount,
    int AdultsCount,
    IReadOnlyCollection<int> ChildrenAges) : IRequest<SearchAvailabilityResult>;
