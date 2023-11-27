using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ITentikaTest.Common.Helpers;

public static class FluentValidatorHelper
{
    public static void Register(IServiceCollection serviceCollection)
    {
        var validators = from type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(v => v.GetTypes())
            where !type.IsAbstract && !type.IsGenericTypeDefinition
            let interfaces = type.GetInterfaces()
            let genericInterfaces =
                interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>))
            let matchingInterface = genericInterfaces.FirstOrDefault()
            where matchingInterface != null
            select new
            {
                InterfaceType = matchingInterface,
                ValidatorType = type
            };
        
        validators.ToList().ForEach(x => serviceCollection.AddSingleton(x.InterfaceType, x.ValidatorType));
    }
}