namespace Chaika.Domain.Entities;

/// <summary>
/// A hotel and the room types it offers.
/// </summary>
public sealed class Hotel
{
    public Hotel(string id, string name, IReadOnlyCollection<Room> rooms)
    {
        Id = id;
        Name = name;
        Rooms = rooms;
    }

    public string Id { get; }

    public string Name { get; }

    public IReadOnlyCollection<Room> Rooms { get; }
}
