using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Chaika.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date",
                Example = new OpenApiString("2026-06-22"),
            });

            options.MapType<DateOnly?>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date",
                Nullable = true,
                Example = new OpenApiString("2026-06-22"),
            });

            options.MapType<DateTimeOffset>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date-time",
                Example = new OpenApiString("2026-06-22T14:00:00+00:00"),
            });

            options.SchemaFilter<DefaultValueSchemaFilter>();
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerWithUi(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}
