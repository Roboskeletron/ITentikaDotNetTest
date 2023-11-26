using ITentikaTest.Common.Settings;
using ITentikaTest.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Context;

public static class Bootstrapper
{
    public static IServiceCollection AddAppDbContext<TDbContext>(this IServiceCollection serviceCollection,
        IConfiguration? configuration = null) where TDbContext : DbContext
    {
        var settings = Settings.Load<DbSettings>($"Database{typeof(TDbContext)}", configuration);

        serviceCollection.AddSingleton(settings);
        
        var dbInitOptionsDelegate = DbContextOptionsFactory.Configure(
            settings.ConnectionString,
            settings.Type);

        serviceCollection.AddDbContextFactory<TDbContext>(dbInitOptionsDelegate);

        return serviceCollection;
    }
}