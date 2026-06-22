using Chaika.Contracts.Requests;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Chaika.Api.Extensions;

public sealed class DefaultValueSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type != typeof(SearchAvailabilityRequest))
        {
            return;
        }

        if (schema.Properties.TryGetValue("hotelId", out var hotelId))
        {
            hotelId.Example = new OpenApiString("hotel-1");
        }

        if (schema.Properties.TryGetValue("roomsCount", out var roomsCount))
        {
            roomsCount.Example = new OpenApiInteger(1);
        }

        if (schema.Properties.TryGetValue("adultsCount", out var adultsCount))
        {
            adultsCount.Example = new OpenApiInteger(1);
        }
    }
}
