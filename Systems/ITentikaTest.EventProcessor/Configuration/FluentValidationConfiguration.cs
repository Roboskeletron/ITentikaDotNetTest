using ITentikaTest.Common.Helpers;
using ITentikaTest.Common.Validators;

namespace ITentikaTest.EventProcessor.Configuration;

public static class FluentValidationConfiguration
{
    public static IServiceCollection AddAppValidators(this IServiceCollection services)
    {
        FluentValidatorHelper.Register(services);

        services.AddSingleton(typeof(IModelValidator<>), typeof(ModelValidator<>));
        
        return services;
    }
}