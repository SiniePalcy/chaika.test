using Chaika.Application.Abstractions;
using Chaika.Application.Exceptions;
using Chaika.Domain.Entities;
using Chaika.Domain.Messaging;
using Chaika.Domain.Results;
using Chaika.Domain.ValueObjects;

namespace Chaika.Application.Services;

/// <summary>
/// Computes available rooms and their total prices from the (mock) hotel repository.
/// </summary>
public sealed class AvailabilityService(IHotelRepository hotelRepository) : IAvailabilityService
{
    public async Task<SearchAvailabilityResult> SearchAsync(
        SearchAvailabilityQuery query,
        CancellationToken cancellationToken)
    {
        var hotel = await hotelRepository.GetByIdAsync(query.HotelId, cancellationToken).ConfigureAwait(false);

        if (hotel is null)
        {
            throw new NotFoundException($"Hotel '{query.HotelId}' was not found.");
        }

        var stay = new StayPeriod(query.CheckInDate, query.CheckOutDate);
        var totalGuests = query.AdultsCount + query.ChildrenAges.Count;

        var availableRooms = hotel.Rooms
            .Where(room => room.CanAccommodate(totalGuests, query.RoomsCount))
            .Select(room => MapRoom(room, stay.Nights, query.RoomsCount))
            .ToList();

        return new SearchAvailabilityResult(hotel.Id, hotel.Name, stay, availableRooms);
    }

    private static AvailableRoomResult MapRoom(Room room, int nights, int roomsCount)
    {
        var offers = room.RatePlans
            .Select(ratePlan => new RatePlanOffer(
                ratePlan.Id,
                ratePlan.Name,
                ratePlan.TotalPrice(nights, roomsCount),
                ratePlan.MealPlan,
                ratePlan.CancellationPolicy))
            .ToList();

        return new AvailableRoomResult(room.Id, room.Name, room.MaxOccupancy, offers);
    }
}
