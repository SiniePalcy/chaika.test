using Chaika.Contracts.Requests;
using Chaika.Contracts.Responses;
using Refit;

namespace Chaika.Client;

/// <summary>
/// Typed Refit client for the Chaika API. Uses the shared contract records.
/// </summary>
public interface IChaikaApi
{
    [Post("/api/availability/search")]
    Task<SearchAvailabilityResponse> SearchAvailabilityAsync(
        [Body] SearchAvailabilityRequest request,
        CancellationToken cancellationToken = default);

    [Post("/api/bookings")]
    Task<CreateBookingResponse> CreateBookingAsync(
        [Body] CreateBookingRequest request,
        CancellationToken cancellationToken = default);
}
