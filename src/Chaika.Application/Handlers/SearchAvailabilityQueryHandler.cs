using Chaika.Application.Abstractions;
using Chaika.Domain.Messaging;
using Chaika.Domain.Results;
using MediatR;

namespace Chaika.Application.Handlers;

/// <summary>
/// Handles <see cref="SearchAvailabilityQuery"/> by delegating to the availability service.
/// </summary>
public sealed class SearchAvailabilityQueryHandler(IAvailabilityService availabilityService)
    : IRequestHandler<SearchAvailabilityQuery, SearchAvailabilityResult>
{
    public Task<SearchAvailabilityResult> Handle(
        SearchAvailabilityQuery query,
        CancellationToken ct)
    {
        return availabilityService.SearchAsync(query, ct);
    }
}
