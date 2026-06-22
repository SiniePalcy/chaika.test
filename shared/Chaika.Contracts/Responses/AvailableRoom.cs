namespace Chaika.Contracts.Responses;

public sealed record AvailableRoom(
    string RoomId,
    string Name,
    int MaxOccupancy,
    IReadOnlyCollection<RatePlanOption> RatePlans);
