using Chaika.Api.Mapping;
using Chaika.Contracts.Requests;
using Chaika.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chaika.Api.Controllers;

[ApiController]
[Route("api/bookings")]
public sealed class BookingsController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Booking creation is intentionally not implemented; the handler raises a 501 Not Implemented.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CreateBookingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status501NotImplemented)]
    public async Task<ActionResult<CreateBookingResponse>> CreateAsync(
        [FromBody] CreateBookingRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(request.ToCommand(), cancellationToken).ConfigureAwait(false);

        return Ok(result);
    }
}
