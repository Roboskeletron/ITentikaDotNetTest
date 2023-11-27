using ITentikaTest.Context;
using ITentikaTest.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Context;

public static class Bootstrapper
{
    public static IServiceCollection AddAppDbContext<TDbContext>(this IServiceCollection services,
        IConfiguration? configuration = null) where TDbContext : DbContext
    {
        var settings = Settings.Load<DbSettings>($"Database{typeof(TDbContext)}", configuration);

        services.AddSingleton(settings);
        
        var dbInitOptionsDelegate = DbContextOptionsFactory.Configure(
            settings.ConnectionString,
            settings.Type);

        services.AddDbContextFactory<TDbContext>(dbInitOptionsDelegate);

        return services;
    }
}