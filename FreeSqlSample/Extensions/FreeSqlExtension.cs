using FreeSql;

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
        services.AddScoped<UnitOfWorkManager>();
        return services;
    }
}