// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using DynamicProxy.Extensions;
using DynamicProxySample.Interceptors;
using DynamicProxySample.Interfaces;
using DynamicProxySample.Repositories;
using DynamicProxySample.Services;
using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

var serviceCollection = new ServiceCollection();
serviceCollection.AddLogging(builder => builder.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Information;
}));
serviceCollection.AddScoped<TestRepository>();
serviceCollection.AddServiceProxy().ConfigureServiceProxy<IServiceA, ServiceA>(builder =>
{
    builder.AddInterceptor<LogInterceptor>(ServiceLifetime.Transient);
    // builder.AddInterceptor<TransactionalInterceptor>(ServiceLifetime.Scoped);
});
// serviceCollection.AddSingleton<IFreeSql>(serviceProvider =>
// {
//     var logger = serviceProvider.GetRequiredService<ILogger<IFreeSql>>();
//     var builder = new FreeSqlBuilder().UseConnectionString(
//         connectionString:
//         "data source=121.37.246.101,7433;initial catalog=acip_iplatform;user id=sa;password=sa@acip.cn;persist security info=True;packet size=4096;TrustServerCertificate=true",
//         dataType: DataType.SqlServer)
//         .UseMonitorCommand(executing: _ => { },(_, s) => logger.LogInformation(s));
//     return builder.Build();
// });
// serviceCollection.AddScoped<UnitOfWorkManager>();

var provider = serviceCollection.BuildServiceProvider();
var service = provider.GetRequiredService<IServiceA>();
var logger = provider.GetRequiredService<ILogger<Program>>();

var a = 2;
// var result = service.Test(ref a);
// logger.LogInformation("result is {Result}", result);
// logger.LogInformation("a is {A}", a);
// service.Test(2);

await service.TestDbAsync(a, out var name);
logger.LogInformation("name is {Name}", name);

await Task.Delay(TimeSpan.FromSeconds(3));