using ITentikaTest.EventProcessor.Configuration;

namespace ITentikaTest.WebAPI.Configuration;

public static class ControllersConfiguration
{
    public static IServiceCollection AddAppControllers(this IServiceCollection services)
    {
        services.AddControllers()
            .AddNewtonsoftJson()
            .AddAppValidators()
            ;

        return services;
    }

    public static IEndpointRouteBuilder UseAppControllers(this IEndpointRouteBuilder app)
    {
        app.MapControllers();

        return app;
    }
}