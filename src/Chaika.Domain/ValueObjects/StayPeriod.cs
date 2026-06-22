namespace Chaika.Domain.ValueObjects;

/// <summary>
/// A check-in / check-out date range. The number of nights is the difference between the two dates.
/// </summary>
public readonly record struct StayPeriod
{
    public StayPeriod(DateOnly checkIn, DateOnly checkOut)
    {
        if (checkOut <= checkIn)
        {
            throw new ArgumentException("Check-out date must be after check-in date.", nameof(checkOut));
        }

        CheckIn = checkIn;
        CheckOut = checkOut;
    }

    public DateOnly CheckIn { get; }

    public DateOnly CheckOut { get; }

    public int Nights => CheckOut.DayNumber - CheckIn.DayNumber;
}
