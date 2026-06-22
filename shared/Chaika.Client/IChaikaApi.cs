using Chaika.Contracts.Requests;
using Chaika.Contracts.Responses;
using Refit;

namespace Chaika.Client;

/// <summary>
/// Typed Refit client for the Chaika API. Uses the shared contract records.
/// </summary>
public interface IChaikaApi
{
    [Get("/api/availability/search")]
    Task<SearchAvailabilityResponse> SearchAvailabilityAsync(
        [Query] SearchAvailabilityRequest request,
        CancellationToken ct = default);

    [Post("/api/bookings")]
    Task<CreateBookingResponse> CreateBookingAsync(
        [Body] CreateBookingRequest request,
        CancellationToken ct = default);
}
