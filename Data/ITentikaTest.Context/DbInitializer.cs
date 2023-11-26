using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Context;

public static class DbInitializer
{
    public static void Execute<TDbContext>(IServiceProvider serviceProvider) where TDbContext : DbContext
    {
        using var scope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
        ArgumentNullException.ThrowIfNull(scope);

        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<TDbContext>>();
        using var context = dbContextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}