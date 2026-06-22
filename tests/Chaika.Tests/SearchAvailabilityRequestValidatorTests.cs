using Chaika.Api.Validation;
using Chaika.Contracts.Requests;
using Chaika.Tests.TestDoubles;
using FluentValidation.Results;

namespace Chaika.Tests;

public sealed class SearchAvailabilityRequestValidatorTests
{
    private static readonly DateOnly Today = new(2026, 6, 22);

    private static SearchAvailabilityRequestValidator CreateValidator()
    {
        return new SearchAvailabilityRequestValidator(new FixedClock(Today));
    }

    private static SearchAvailabilityRequest ValidRequest()
    {
        return new SearchAvailabilityRequest(
            HotelId: "hotel-1",
            CheckInDate: new DateOnly(2026, 7, 1),
            CheckOutDate: new DateOnly(2026, 7, 5),
            RoomsCount: 1,
            AdultsCount: 2,
            ChildrenAges: new[] { 7 });
    }

    [Fact]
    public void Valid_request_passes()
    {
        ValidationResult result = CreateValidator().Validate(ValidRequest());

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Check_in_date_in_the_past_is_rejected()
    {
        var request = ValidRequest() with { CheckInDate = Today.AddDays(-1), CheckOutDate = Today.AddDays(2) };

        Assert.False(CreateValidator().Validate(request).IsValid);
    }

    [Fact]
    public void Check_out_equal_to_check_in_is_rejected()
    {
        var request = ValidRequest() with { CheckOutDate = ValidRequest().CheckInDate };

        Assert.False(CreateValidator().Validate(request).IsValid);
    }

    [Fact]
    public void Check_out_before_check_in_is_rejected()
    {
        var request = ValidRequest() with { CheckOutDate = ValidRequest().CheckInDate.AddDays(-1) };

        Assert.False(CreateValidator().Validate(request).IsValid);
    }

    [Fact]
    public void Stay_longer_than_one_month_is_rejected()
    {
        var checkIn = new DateOnly(2026, 7, 1);
        var request = ValidRequest() with { CheckInDate = checkIn, CheckOutDate = checkIn.AddDays(40) };

        Assert.False(CreateValidator().Validate(request).IsValid);
    }

    [Fact]
    public void Booking_more_than_one_year_ahead_is_rejected()
    {
        var checkIn = Today.AddYears(1).AddDays(1);
        var request = ValidRequest() with { CheckInDate = checkIn, CheckOutDate = checkIn.AddDays(2) };

        Assert.False(CreateValidator().Validate(request).IsValid);
    }

    [Fact]
    public void Empty_hotel_id_is_rejected()
    {
        var request = ValidRequest() with { HotelId = string.Empty };

        Assert.False(CreateValidator().Validate(request).IsValid);
    }

    [Fact]
    public void Adults_count_of_zero_is_rejected()
    {
        var request = ValidRequest() with { AdultsCount = 0 };

        Assert.False(CreateValidator().Validate(request).IsValid);
    }

    [Fact]
    public void Rooms_count_of_zero_is_rejected()
    {
        var request = ValidRequest() with { RoomsCount = 0 };

        Assert.False(CreateValidator().Validate(request).IsValid);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(18)]
    public void Invalid_children_age_is_rejected(int age)
    {
        var request = ValidRequest() with { ChildrenAges = new[] { age } };

        Assert.False(CreateValidator().Validate(request).IsValid);
    }

    [Fact]
    public void Null_children_ages_are_allowed()
    {
        var request = ValidRequest() with { ChildrenAges = null };

        Assert.True(CreateValidator().Validate(request).IsValid);
    }
}
