namespace Chaika.Contracts.Responses;

public sealed record ErrorResponse(
    string Code,
    string Message);
