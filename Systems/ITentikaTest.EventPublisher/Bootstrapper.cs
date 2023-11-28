using ITentikaTest.Common.Settings;
using ITentikaTest.WebAPI.Services.EventService;

namespace ITentikaTest.WebAPI;

public static class Bootstrapper
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var eventServiceSettings = Settings.Settings.Load<MicroserviceSettings>("EventProcessor", configuration);

        services.AddSingleton(eventServiceSettings);

        services.AddSingleton<IEventService, EventService>();
        
        return services;
    }
}