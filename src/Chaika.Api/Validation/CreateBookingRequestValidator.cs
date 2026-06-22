using Chaika.Application.Abstractions;
using Chaika.Contracts.Requests;
using FluentValidation;

namespace Chaika.Api.Validation;

/// <summary>
/// Validates the create-booking request. The endpoint itself is not implemented, but requests are still validated.
/// </summary>
public sealed class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
{
    public CreateBookingRequestValidator(IClock clock)
    {
        RuleFor(x => x.HotelId)
            .NotEmpty();

        RuleFor(x => x.RoomId)
            .NotEmpty();

        RuleFor(x => x.RatePlanId)
            .NotEmpty();

        RuleFor(x => x.CheckInDate)
            .GreaterThanOrEqualTo(clock.Today)
            .WithMessage("Check-in date must be today or later.");

        RuleFor(x => x.CheckOutDate)
            .GreaterThan(x => x.CheckInDate)
            .WithMessage("Check-out date must be after check-in date.");

        RuleFor(x => x.RoomsCount)
            .GreaterThan(0);

        RuleFor(x => x.AdultsCount)
            .GreaterThan(0);

        RuleForEach(x => x.ChildrenAges)
            .InclusiveBetween(0, 17)
            .When(x => x.ChildrenAges is not null);

        RuleFor(x => x.Customer)
            .NotNull();

        When(x => x.Customer is not null, () =>
        {
            RuleFor(x => x.Customer.FirstName)
                .NotEmpty();

            RuleFor(x => x.Customer.LastName)
                .NotEmpty();

            RuleFor(x => x.Customer.Email)
                .NotEmpty()
                .EmailAddress();
        });
    }
}
