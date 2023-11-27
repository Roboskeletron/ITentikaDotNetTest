using Microsoft.Extensions.DependencyInjection;

namespace ITentikaTest.Common.Helpers;

public static class AutoMapperHelper
{
    public static void Register(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("itentikatest."));

        services.AddAutoMapper(assemblies);
    }
}