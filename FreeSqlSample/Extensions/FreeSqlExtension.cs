using System.Data;
using FreeSql;
using FreeSqlSample.Common;
using FreeSqlSample.Repositories;

namespace FreeSqlSample.Extensions;

public static class FreeSqlExtension
{
    public static IServiceCollection AddFreeSql(this IServiceCollection services, IConfiguration configuration, string applicationName = "master",  params string[] dbNames)
    {
        services.AddSingleton(provider =>
        {
            var freeSql = new FreeSqlCloud<string>(applicationName);
            foreach (var dbName in dbNames)
            {
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger($"FreeSql.{dbName}");
                freeSql.Register(dbName,
                    new FreeSqlBuilder().UseConnectionString(DataType.SqlServer, configuration.GetConnectionString(dbName))
                        .UseMonitorCommand(executing: command => logger.LogInformation(command.CommandText))
                        .Build);
            }

            return freeSql;
        });
        services.AddSingleton<IFreeSql, FreeSqlCloud<string>>(provider =>
            provider.GetRequiredService<FreeSqlCloud<string>>());
        services.AddScoped<UnitOfWorkManagerCloud<string>>();
        return services;
    }

    public static IServiceCollection AddFreeSql<T>(this IServiceCollection services, string connectionString,
        DataType dbType) where T : IDbKey, new()
    {
        services.AddSingleton(provider =>
        {
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger($"FreeSql.{new T().Name}");
            var freeSql = new FreeSqlBuilder().UseConnectionString(dbType, connectionString)
                .UseMonitorCommand(command => logger.LogInformation(command.CommandText)).Build<T>();
            return freeSql;
        });

        services.AddScoped<UnitOrWorkManager<T>>();

        return services;
    }
}