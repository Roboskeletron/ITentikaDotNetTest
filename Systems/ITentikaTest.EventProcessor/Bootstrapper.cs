using Context;
using ITentikaTest.EventProcessor.Configuration;
using ITentikaTest.EventProcessor.Services;
using ITentikaTest.EventProcessor.Services.EventService;
using ITentikaTest.EventProcessor.Services.Facories;

namespace ITentikaTest.EventProcessor;

public static class Bootstrapper
{
    public static IServiceCollection AddAppServices(this IServiceCollection services,
        IConfiguration? configuration = null)
    {
        services
            .AddAppDbContext<EventProcessorDbContext>(configuration)
            .AddAppValidators()
            .AddSingleton<IIncidentFactory, IncidentFactory>()
            .AddSingleton<IEventService, EventService>()
            .AddHostedService<EventProcessorService>()
            ;

        return services;
    }
}