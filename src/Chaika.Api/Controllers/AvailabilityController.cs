using Chaika.Api.Mapping;
using Chaika.Contracts.Requests;
using Chaika.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chaika.Api.Controllers;

[ApiController]
[Route("api/availability")]
public sealed class AvailabilityController(ISender sender) : ControllerBase
{
    [HttpGet("search")]
    [ProducesResponseType(typeof(SearchAvailabilityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SearchAvailabilityResponse>> SearchAsync(
        [FromQuery] SearchAvailabilityRequest request,
        CancellationToken ct)
    {
        var result = await sender.Send(request.ToQuery(), ct).ConfigureAwait(false);

        return Ok(result.ToResponse());
    }
}
