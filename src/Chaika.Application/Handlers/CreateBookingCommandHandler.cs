using Chaika.Application.Exceptions;
using Chaika.Domain.Messaging;
using Chaika.Domain.Results;
using MediatR;

namespace Chaika.Application.Handlers;

/// <summary>
/// Handles <see cref="CreateBookingCommand"/>. Booking creation is intentionally not implemented for this task.
/// </summary>
public sealed class CreateBookingCommandHandler
    : IRequestHandler<CreateBookingCommand, CreateBookingResult>
{
    public Task<CreateBookingResult> Handle(
        CreateBookingCommand command,
        CancellationToken ct)
    {
        throw new NotImplementedFeatureException("Booking creation is not implemented.");
    }
}
