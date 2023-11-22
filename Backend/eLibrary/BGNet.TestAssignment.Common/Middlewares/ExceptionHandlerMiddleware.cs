using BGNet.TestAssignment.Models.Responses;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace BGNet.TestAssignment.Common.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _request;

    public ExceptionHandlerMiddleware(RequestDelegate request)
    {
        _request = request;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _request(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (status, errorMessage) = exception switch
        {
            ApplicationException _ => (HttpStatusCode.BadRequest, "Application exception occurred."),
            KeyNotFoundException _ => (HttpStatusCode.NotFound, "The request key not found."),
            UnauthorizedAccessException _ => (HttpStatusCode.Unauthorized, "Unauthorized."),
            _ => (HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        var response = new ApiResponse
        {
            StatusCode = context.Response.StatusCode,
            Errors = new[] { errorMessage },
        };

        await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(response));
    }
}
