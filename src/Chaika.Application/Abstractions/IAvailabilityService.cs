using Chaika.Domain.Messaging;
using Chaika.Domain.Results;

namespace Chaika.Application.Abstractions;

/// <summary>
/// Application service that computes room availability and pricing for a search query.
/// </summary>
public interface IAvailabilityService
{
    Task<SearchAvailabilityResult> SearchAsync(SearchAvailabilityQuery query, CancellationToken ct);
}
