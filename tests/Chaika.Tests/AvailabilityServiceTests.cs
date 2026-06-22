using Chaika.Application.Exceptions;
using Chaika.Application.Services;
using Chaika.Domain.Messaging;
using Chaika.Infrastructure.Repositories;

namespace Chaika.Tests;

public sealed class AvailabilityServiceTests
{
    private static AvailabilityService CreateService()
    {
        return new AvailabilityService(new MockHotelRepository());
    }

    private static SearchAvailabilityQuery Query(string hotelId = "hotel-1")
    {
        return new SearchAvailabilityQuery(
            hotelId,
            new DateOnly(2026, 7, 1),
            new DateOnly(2026, 7, 5),
            RoomsCount: 1,
            AdultsCount: 2,
            ChildrenAges: Array.Empty<int>());
    }

    [Fact]
    public async Task Unknown_hotel_throws_NotFoundException()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<NotFoundException>(
            () => service.SearchAsync(Query("does-not-exist"), CancellationToken.None));
    }

    [Fact]
    public async Task Available_rooms_are_returned()
    {
        var service = CreateService();

        var result = await service.SearchAsync(Query(), CancellationToken.None);

        Assert.Equal("hotel-1", result.HotelId);
        Assert.Equal(4, result.Stay.Nights);
        Assert.NotEmpty(result.Rooms);
        Assert.All(result.Rooms, room => Assert.True(room.MaxOccupancy >= 2));
    }

    [Fact]
    public async Task Rooms_that_cannot_accommodate_guests_are_excluded()
    {
        var service = CreateService();
        // hotel-2 has only a single room (max occupancy 1); two adults cannot be accommodated.
        var result = await service.SearchAsync(Query("hotel-2"), CancellationToken.None);

        Assert.Empty(result.Rooms);
    }

    [Fact]
    public async Task Total_price_is_price_per_night_times_nights_times_rooms()
    {
        var service = CreateService();
        var query = Query() with { RoomsCount = 2 };

        var result = await service.SearchAsync(query, CancellationToken.None);

        var standardRoom = result.Rooms.Single(r => r.RoomId == "room-standard");
        var bedAndBreakfast = standardRoom.RatePlans.Single(r => r.RatePlanId == "rate-standard-bb");

        // 120 per night * 4 nights * 2 rooms = 960
        Assert.Equal(960m, bedAndBreakfast.TotalPrice.Amount);
        Assert.Equal("EUR", bedAndBreakfast.TotalPrice.Currency);
    }
}
