using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Context;

public static class Bootstrapper
{
    public static IServiceCollection AddAppDbContext<TDbContext>(this IServiceCollection serviceCollection,
        IConfiguration configuration = null) where TDbContext : DbContext
    {
        return serviceCollection;
    }
}