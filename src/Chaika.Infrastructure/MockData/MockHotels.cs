using Chaika.Domain.Entities;
using Chaika.Domain.Enums;
using Chaika.Domain.ValueObjects;

namespace Chaika.Infrastructure.MockData;

/// <summary>
/// Static, in-memory hotel data used in place of a real database.
/// </summary>
public static class MockHotels
{
    private const string Currency = "EUR";

    public static IReadOnlyList<Hotel> All { get; } = Build();

    private static IReadOnlyList<Hotel> Build()
    {
        var refundableUntil = new DateTimeOffset(2026, 6, 28, 12, 0, 0, TimeSpan.Zero);

        var seaViewHotel = new Hotel(
            "hotel-1",
            "Chaika Sea View",
            new List<Room>
            {
                new(
                    "room-standard",
                    "Standard Double",
                    maxOccupancy: 2,
                    new List<RatePlan>
                    {
                        new(
                            "rate-standard-bb",
                            "Bed & Breakfast (Flexible)",
                            new Money(120m, Currency),
                            MealPlan.Breakfast,
                            CancellationPolicy.RefundableUntil(refundableUntil)),
                        new(
                            "rate-standard-ro",
                            "Room Only (Non-refundable)",
                            new Money(95m, Currency),
                            MealPlan.RoomOnly,
                            CancellationPolicy.NonRefundable),
                    }),
                new(
                    "room-family",
                    "Family Suite",
                    maxOccupancy: 4,
                    new List<RatePlan>
                    {
                        new(
                            "rate-family-hb",
                            "Half Board (Flexible)",
                            new Money(210m, Currency),
                            MealPlan.HalfBoard,
                            CancellationPolicy.RefundableUntil(refundableUntil)),
                    }),
            });

        var cityHotel = new Hotel(
            "hotel-2",
            "Chaika City Center",
            new List<Room>
            {
                new(
                    "room-single",
                    "Cozy Single",
                    maxOccupancy: 1,
                    new List<RatePlan>
                    {
                        new(
                            "rate-single-ro",
                            "Room Only (Non-refundable)",
                            new Money(70m, Currency),
                            MealPlan.RoomOnly,
                            CancellationPolicy.NonRefundable),
                    }),
            });

        return new List<Hotel> { seaViewHotel, cityHotel };
    }
}
