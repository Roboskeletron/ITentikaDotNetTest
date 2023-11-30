using Context;
using ITentikaTest.EventProcessor.Services;
using ITentikaTest.EventProcessor.Services.EventService;
using ITentikaTest.EventProcessor.Services.Facories;
using ITentikaTest.EventProcessor.settings;

namespace ITentikaTest.EventProcessor;

public static class Bootstrapper
{
    public static IServiceCollection AddAppServices(this IServiceCollection services,
        IConfiguration? configuration = null)
    {
        var incidentFactorySettings = Settings.Settings.Load<IncidentFactrorySettings>("IncidentFactory", configuration);
        
        services
            .AddSingleton(incidentFactorySettings)
            .AddAppDbContext<EventProcessorDbContext>(configuration)
            .AddSingleton<IIncidentFactory, IncidentFactory>()
            .AddSingleton<IEventService, EventService>()
            .AddHostedService<EventProcessorService>()
            ;

        return services;
    }
}