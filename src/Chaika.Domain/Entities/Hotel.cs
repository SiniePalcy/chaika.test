namespace Chaika.Domain.Entities;

/// <summary>
/// A hotel and the room types it offers.
/// </summary>
public sealed class Hotel(string id, string name, IReadOnlyCollection<Room> rooms)
{
    public string Id { get; } = id;

    public string Name { get; } = name;

    public IReadOnlyCollection<Room> Rooms { get; } = rooms;
}
