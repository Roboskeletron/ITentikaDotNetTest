using ITentikaTest.Common.Helpers;

namespace ITentikaTest.WebAPI.Configuration;

public static class AutoMapperConfiguration
{
    public static IServiceCollection AddAppAutoMappers(this IServiceCollection services)
    {
        AutoMapperHelper.Register(services);
        return services;
    }
}