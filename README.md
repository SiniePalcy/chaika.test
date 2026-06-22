# chaika.test

Small test task API for hotel room availability search, built with C# and .NET 10.

> **Prerequisite:** the .NET 10 SDK. The target framework is set once in
> [`Directory.Build.props`](Directory.Build.props) (`net10.0`) and applies to every project, so retargeting
> the whole solution is a single-line change.

## Task Description

Create a small REST API using C# and .NET 10.

### Availability Search

Implement an endpoint for searching available rooms in a specific hotel.

Search parameters:

- Hotel
- Check-in and check-out dates
- Number of rooms
- Number of adults
- Children ages, if the booking includes children

A booking can start today and can be created no more than one year in advance.
The maximum stay duration is one month.

For an invalid request, the API must return an appropriate error.

The search result must contain a list of available rooms.
For each room, the API must return its name and one or more rate plans.

Each rate plan must contain:

- Price for the whole stay period
- Cancellation policy:
  - non-refundable; or
  - free cancellation until a specific date and time
- Meal information, if meal is included in the rate

The most suitable types and model structure should be proposed for representing the request, rooms, rate plans, prices, cancellation policies, and other required data.

Test data may be mocked or generated dynamically.

### Booking Creation

Also define an endpoint and request types required to create a booking based on the selected room and rate plan.

Booking creation does not need to be implemented.

The endpoint must return an error indicating that this functionality is not implemented yet.

### Implementation Expectations

The solution should demonstrate:

- Usage of correct types for storing and transferring data
- Understanding of nullable reference types and correct null handling
- Nullable context enabled via `<Nullable>enable</Nullable>`
- Usage of `record`
- Understanding of dependency injection principles
- Understanding of the purpose and correct usage of `CancellationToken`
- Understanding of asynchronous programming and correct usage of `async` / `await`

Any developer tools are allowed.

## What is implemented

- Search available rooms for a selected hotel and stay period (`GET /api/availability/search`)
- Return available rooms with rate plans, total price, cancellation policy and meal plan
- Booking creation endpoint and request model (`POST /api/bookings`) that returns `501 Not Implemented`
- Mock data instead of a real database
- Centralized exception-handling middleware
- FluentValidation request validation via an action filter
- MediatR-based command/query dispatch (controllers send through `ISender`)
- Shared, transport-only `Contracts` records and a typed Refit `Client`

## Architecture

The solution follows Clean Architecture:

```text
src/
  Chaika.Api/             Controllers, validators, validation filter, exception middleware, DI, startup
  Chaika.Application/     MediatR handlers, application services, application exceptions
  Chaika.Domain/          Entities, value objects, MediatR command/query records, results
  Chaika.Infrastructure/  Mock repository, mock data, system clock, DI

shared/
  Chaika.Contracts/       Transport-only request/response records shared by API and client
  Chaika.Client/          Refit API interface + DI registration (references Contracts only)

tests/
  Chaika.Tests/           Unit + integration tests (xUnit)
```

Dependency direction:

```text
Api -> Application -> Domain
Api -> Infrastructure -> Application / Domain
Api -> Contracts
Client -> Contracts (+ Refit)
Domain -> MediatR abstractions only
```

The Domain has no dependency on ASP.NET Core, Infrastructure, EF Core, Refit, a database, or API DTOs —
its only external dependency is `MediatR.Contracts` for the command/query request records.

## Main API

### Search availability

```http
GET /api/availability/search
```

Example request:

```
http://localhost:5045/api/availability/search?HotelId=hotel-1&CheckInDate=2026-06-23&CheckOutDate=2026-06-24&RoomsCount=1&AdultsCount=1
```

Returns `200 OK` with available rooms, `400 Bad Request` for invalid input, or `404 Not Found` for an unknown hotel.

### Create booking

```http
POST /api/bookings
```

Example request body:

```json
{
  "hotelId": "hotel-1",
  "roomId": "room-family",
  "ratePlanId": "rate-family-hb",
  "checkInDate": "2026-07-15",
  "checkOutDate": "2026-07-18",
  "roomsCount": 1,
  "adultsCount": 2,
  "childrenAges": [5, 8],
  "customer": {
    "firstName": "Maria",
    "lastName": "Garcia",
    "email": "maria.garcia@example.com"
  }
}
```

Booking creation is intentionally not implemented for this task.

Expected response:

```http
501 Not Implemented
```

## Error format

All handled exceptions return an `ErrorResponse`:

```json
{ "code": "not_found", "message": "Hotel 'x' was not found." }
```

| Exception                        | Status |
| -------------------------------- | ------ |
| `ValidationException`            | 400    |
| `NotFoundException`              | 404    |
| `NotImplementedFeatureException` | 501    |
| any other exception              | 500    |

## Technical Notes

- Nullable reference types are enabled across all projects
- Commands, queries, requests, responses and value objects are `record`s; entities are classes
- Controllers are thin and contain no business logic
- Business logic lives in the Application layer; mock data lives in Infrastructure
- Domain models are independent from ASP.NET Core and infrastructure
- Exceptions are handled by centralized middleware (no try/catch in controllers)
- `CancellationToken` is passed through API, application and infrastructure layers
- Null children ages are normalized to an empty collection before building the query

## Mock Data

No database is required. Mock data is stored in Infrastructure:

```text
src/Chaika.Infrastructure/MockData/MockHotels.cs
```

`MockHotelRepository` reads hotels from this static collection and can later be replaced with a real
repository implementation. Available hotels: `hotel-1` (Chaika Sea View), `hotel-2` (Chaika City Center).

## Run

```bash
dotnet restore
dotnet build
dotnet run --project src/Chaika.Api
```

Swagger UI is available in the Development environment at `/swagger`.

## Test

```bash
dotnet test
```

## Typed client usage

```csharp
services.AddChaikaClient(new Uri("https://localhost:5001"));
// inject IChaikaApi
var response = await api.SearchAvailabilityAsync(request, cancellationToken);
```

## Validation

The availability search validates:

- hotel id is required
- check-in date is today or later
- check-out date is after check-in date
- booking is not more than one year ahead
- stay duration is not longer than one month
- rooms count is greater than zero
- adults count is greater than zero
- children ages are between 0 and 17
