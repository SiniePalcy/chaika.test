using Chaika.Domain.Entities;

namespace Chaika.Application.Abstractions;

/// <summary>
/// Read access to hotels. Implemented with mock data in the Infrastructure layer.
/// </summary>
public interface IHotelRepository
{
    Task<Hotel?> GetByIdAsync(string hotelId, CancellationToken cancellationToken);
}
