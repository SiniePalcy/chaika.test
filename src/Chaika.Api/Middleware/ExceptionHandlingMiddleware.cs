using System.Text.Json;
using Chaika.Application.Exceptions;
using Chaika.Contracts.Responses;
using FluentValidation;

namespace Chaika.Api.Middleware;

/// <summary>
/// Centralized exception handling. Maps known exceptions to <see cref="ErrorResponse"/> with the right status code.
/// </summary>
public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context).ConfigureAwait(false);
        }
        catch (ValidationException exception)
        {
            var message = string.Join(" ", exception.Errors.Select(error => error.ErrorMessage).Distinct());
            await WriteResponseAsync(
                context,
                StatusCodes.Status400BadRequest,
                new ErrorResponse("validation_error", message)).ConfigureAwait(false);
        }
        catch (NotFoundException exception)
        {
            await WriteResponseAsync(
                context,
                StatusCodes.Status404NotFound,
                new ErrorResponse("not_found", exception.Message)).ConfigureAwait(false);
        }
        catch (NotImplementedFeatureException exception)
        {
            await WriteResponseAsync(
                context,
                StatusCodes.Status501NotImplemented,
                new ErrorResponse("not_implemented", exception.Message)).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled exception while processing {Path}.", context.Request.Path);
            await WriteResponseAsync(
                context,
                StatusCodes.Status500InternalServerError,
                new ErrorResponse("internal_error", "An unexpected error occurred.")).ConfigureAwait(false);
        }
    }

    private static async Task WriteResponseAsync(HttpContext context, int statusCode, ErrorResponse body)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var json = JsonSerializer.Serialize(body, JsonOptions);
        await context.Response.WriteAsync(json).ConfigureAwait(false);
    }

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
}
