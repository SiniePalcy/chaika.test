using System.Net;
using System.Net.Http.Json;
using Chaika.Contracts.Requests;
using Chaika.Contracts.Responses;

namespace Chaika.Tests;

public sealed class ApiEndpointsTests(ChaikaApiFactory factory) : IClassFixture<ChaikaApiFactory>
{
    [Fact]
    public async Task Search_returns_available_rooms()
    {
        var client = factory.CreateClient();
        var request = new SearchAvailabilityRequest(
            "hotel-1",
            new DateOnly(2026, 7, 1),
            new DateOnly(2026, 7, 5),
            RoomsCount: 1,
            AdultsCount: 2,
            ChildrenAges: null);

        var response = await client.PostAsJsonAsync("/api/availability/search", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<SearchAvailabilityResponse>();
        Assert.NotNull(body);
        Assert.Equal("hotel-1", body!.HotelId);
        Assert.NotEmpty(body.Rooms);
    }

    [Fact]
    public async Task Search_with_invalid_request_returns_400()
    {
        var client = factory.CreateClient();
        var request = new SearchAvailabilityRequest(
            HotelId: string.Empty,
            CheckInDate: new DateOnly(2026, 7, 1),
            CheckOutDate: new DateOnly(2026, 7, 5),
            RoomsCount: 0,
            AdultsCount: 0,
            ChildrenAges: null);

        var response = await client.PostAsJsonAsync("/api/availability/search", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(error);
        Assert.Equal("validation_error", error!.Code);
    }

    [Fact]
    public async Task Search_for_unknown_hotel_returns_404()
    {
        var client = factory.CreateClient();
        var request = new SearchAvailabilityRequest(
            "missing-hotel",
            new DateOnly(2026, 7, 1),
            new DateOnly(2026, 7, 5),
            RoomsCount: 1,
            AdultsCount: 2,
            ChildrenAges: null);

        var response = await client.PostAsJsonAsync("/api/availability/search", request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Create_booking_returns_501_not_implemented()
    {
        var client = factory.CreateClient();
        var request = new CreateBookingRequest(
            "hotel-1",
            "room-standard",
            "rate-standard-bb",
            new DateOnly(2026, 7, 1),
            new DateOnly(2026, 7, 5),
            RoomsCount: 1,
            AdultsCount: 2,
            ChildrenAges: null,
            Customer: new CustomerInfo("Ada", "Lovelace", "ada@example.com"));

        var response = await client.PostAsJsonAsync("/api/bookings", request);

        Assert.Equal(HttpStatusCode.NotImplemented, response.StatusCode);
        var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(error);
        Assert.Equal("not_implemented", error!.Code);
    }
}
