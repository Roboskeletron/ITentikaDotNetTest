using ITentikaTest.Common.Helpers;

namespace ITentikaTest.WebAPI.Configuration;

public static class FluentValidationConfiguration
{
    public static IServiceCollection AddAppValidators(this IServiceCollection services)
    {
        FluentValidatorHelper.Register(services);
        
        return services;
    }
}