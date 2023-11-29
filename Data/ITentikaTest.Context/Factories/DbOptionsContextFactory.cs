using Microsoft.EntityFrameworkCore;

namespace ITentikaTest.Context;

public static class DbContextOptionsFactory
{
    private const string migrationProjctPrefix = "ITentikaTest.Context.Migrations";
    
    public static DbContextOptions<T> Create<T>(string connStr, DbType dbType) where T : DbContext
    {
        var builder = new DbContextOptionsBuilder<T>();

        Configure(connStr, dbType).Invoke(builder);

        return builder.Options;
    }
    
    public static Action<DbContextOptionsBuilder> Configure(string conectionString, DbType dbType)
    {
        return (builder) =>
        {
            switch (dbType)
            {
                case DbType.PostgreSQL:
                    builder.UseNpgsql(conectionString, options =>
                        options.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                            .MigrationsHistoryTable("_ef_migrations_history", "public")
                            .MigrationsAssembly($"{migrationProjctPrefix}.{DbType.PostgreSQL.ToString()}")
                    );
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dbType), dbType, null);
            }

            builder.EnableSensitiveDataLogging();
            builder.UseLazyLoadingProxies();
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        };
    }
}
