using Chaika.Contracts.Requests;
using Chaika.Contracts.Responses;
using Chaika.Domain.Enums;
using Chaika.Domain.Messaging;
using Chaika.Domain.Results;
using Chaika.Domain.ValueObjects;

namespace Chaika.Api.Mapping;

/// <summary>
/// Maps between shared transport contracts and Domain messages/results. Keeps controllers thin.
/// </summary>
public static class ContractMapping
{
    public static SearchAvailabilityQuery ToQuery(this SearchAvailabilityRequest request)
    {
        return new SearchAvailabilityQuery(
            request.HotelId,
            request.CheckInDate,
            request.CheckOutDate,
            request.RoomsCount,
            request.AdultsCount,
            request.ChildrenAges ?? Array.Empty<int>());
    }

    public static CreateBookingCommand ToCommand(this CreateBookingRequest request)
    {
        var guests = new GuestComposition(request.RoomsCount, request.AdultsCount, request.ChildrenAges);
        var customer = new Customer(request.Customer.FirstName, request.Customer.LastName, request.Customer.Email);

        return new CreateBookingCommand(
            request.HotelId,
            request.RoomId,
            request.RatePlanId,
            request.CheckInDate,
            request.CheckOutDate,
            guests,
            customer);
    }

    public static SearchAvailabilityResponse ToResponse(this SearchAvailabilityResult result)
    {
        var rooms = result.Rooms
            .Select(room => new AvailableRoom(
                room.RoomId,
                room.Name,
                room.MaxOccupancy,
                room.RatePlans.Select(ToRatePlanOption).ToList()))
            .ToList();

        return new SearchAvailabilityResponse(
            result.HotelId,
            result.HotelName,
            result.Stay.CheckIn,
            result.Stay.CheckOut,
            result.Stay.Nights,
            rooms);
    }

    private static RatePlanOption ToRatePlanOption(RatePlanOffer offer)
    {
        return new RatePlanOption(
            offer.RatePlanId,
            offer.Name,
            offer.TotalPrice.Amount,
            offer.TotalPrice.Currency,
            offer.MealPlan.ToString(),
            new CancellationPolicyInfo(
                offer.CancellationPolicy.IsRefundable,
                offer.CancellationPolicy.FreeCancellationUntil));
    }
}
