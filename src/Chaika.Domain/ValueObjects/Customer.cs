namespace Chaika.Domain.ValueObjects;

/// <summary>
/// The person a booking is made for.
/// </summary>
public sealed record Customer(string FirstName, string LastName, string Email);
