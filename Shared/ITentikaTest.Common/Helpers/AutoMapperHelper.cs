using Microsoft.Extensions.DependencyInjection;

namespace ITentikaTest.Common.Helpers;

public static class AutoMapperHelper
{
    public static void Register(IServiceCollection serviceCollection)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("itentikatest."));

        serviceCollection.AddAutoMapper(assemblies);
    }
}