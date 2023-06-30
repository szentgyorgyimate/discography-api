using System.Net;
using WebAPI.Common.Responses;

namespace WebAPI.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;

            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await response.WriteAsJsonAsync(new ErrorResponse("An error occured while calling the service."));
        }
    }
}
