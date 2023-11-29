using System.Text.Json;
using FluentValidation;
using ITentikaTest.Common.Extensions;
using ITentikaTest.Common.Responses;

namespace ITentikaTest.WebAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ErrorResponse? errorResponse = null;
        try
        {
            await next.Invoke(context);
        }
        catch (ValidationException validationException)
        {
            errorResponse = validationException.ToErrorResponse();
        }
        catch (Exception exception)
        {
            errorResponse = exception.ToErrorResponse();
        }
        finally
        {
            if (errorResponse != null)
            {
                context.Response.StatusCode = errorResponse.Code;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                await context.Response.StartAsync();
                await context.Response.CompleteAsync();
            }
        }
    }
}