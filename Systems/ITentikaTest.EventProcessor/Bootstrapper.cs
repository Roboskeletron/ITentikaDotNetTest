using Context;

namespace ITentikaTest.EventProcessor;

public static class Bootstrapper
{
    public static IServiceCollection AddAppServices(this IServiceCollection services,
        IConfiguration? configuration = null)
    {
        services.AddAppDbContext<EventProcessorDbContext>(configuration);
        
        return services;
    }
}