using Chaika.Application.Abstractions;
using Chaika.Domain.Entities;
using Chaika.Infrastructure.MockData;

namespace Chaika.Infrastructure.Repositories;

/// <summary>
/// <see cref="IHotelRepository"/> implementation backed by static mock data.
/// </summary>
public sealed class MockHotelRepository : IHotelRepository
{
    public Task<Hotel?> GetByIdAsync(string hotelId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var hotel = MockHotels.All.FirstOrDefault(x => x.Id == hotelId);

        return Task.FromResult(hotel);
    }
}
