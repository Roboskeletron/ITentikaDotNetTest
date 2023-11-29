using ITentikaTest.WebAPI.Middlewares;

namespace ITentikaTest.EventProcessor.Configuration;

public static class MiddlewaresConfiguration
{
    public static void UseAppMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}