using ITentikaTest.Common.Settings;
using ITentikaTest.WebAPI.Services;
using ITentikaTest.WebAPI.Services.EventService;
using ITentikaTest.WebAPI.Settings;

namespace ITentikaTest.WebAPI;

public static class Bootstrapper
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var eventServiceSettings = ITentikaTest.Settings.Settings.Load<MicroserviceSettings>("EventProcessor", configuration);
        var eventGeneratorSettings = ITentikaTest.Settings.Settings.Load<EventGeneratorSettings>("EventGenerator", configuration);

        services
            .AddSingleton(eventServiceSettings)
            .AddSingleton(eventGeneratorSettings)
            .AddSingleton<IEventService, EventService>()
            .AddHostedService<EventGeneratorService>()
            ;
        
        return services;
    }
}