using System.Net;
using System.Text.Json;
using Contact.Application.Exceptions;
using FluentValidation;

namespace Contact.Api.Middlewares;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case ValidationException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case ContactNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new
            {
                source = error.Source,
                message = error.Message
            });
            await response.WriteAsync(result);
        }
    }
}