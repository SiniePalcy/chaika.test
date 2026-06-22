using Chaika.Application.Abstractions;
using Chaika.Contracts.Requests;
using FluentValidation;

namespace Chaika.Api.Validation;

/// <summary>
/// Validates the availability search request. Uses <see cref="IClock"/> for deterministic date rules.
/// </summary>
public sealed class SearchAvailabilityRequestValidator : AbstractValidator<SearchAvailabilityRequest>
{
    public SearchAvailabilityRequestValidator(IClock clock)
    {
        RuleFor(x => x.HotelId)
            .NotEmpty();

        RuleFor(x => x.CheckInDate)
            .GreaterThanOrEqualTo(clock.Today)
            .WithMessage("Check-in date must be today or later.");

        RuleFor(x => x.CheckOutDate)
            .GreaterThan(x => x.CheckInDate)
            .WithMessage("Check-out date must be after check-in date.");

        RuleFor(x => x.CheckInDate)
            .LessThanOrEqualTo(clock.Today.AddYears(1))
            .WithMessage("Check-in date must not be more than one year ahead.");

        RuleFor(x => x)
            .Must(x => x.CheckOutDate.DayNumber - x.CheckInDate.DayNumber <= 31)
            .WithMessage("Stay duration must not exceed one month.");

        RuleFor(x => x.RoomsCount)
            .GreaterThan(0);

        RuleFor(x => x.AdultsCount)
            .GreaterThan(0);

        RuleForEach(x => x.ChildrenAges)
            .InclusiveBetween(0, 17)
            .When(x => x.ChildrenAges is not null);
    }
}
