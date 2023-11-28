using ITentikaTest.Common.Helpers;

namespace ITentikaTest.EventProcessor.Configuration;

public static class AutoMapperConfiguration
{
    public static IServiceCollection AddAppAutoMappers(this IServiceCollection services)
    {
        AutoMapperHelper.Register(services);
        return services;
    }
}