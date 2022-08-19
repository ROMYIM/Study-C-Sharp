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
    options.LogToStandardErrorThreshold = LogLevel.Error;
}));
serviceCollection.AddScoped<TestRepository>();
serviceCollection.AddServiceProxy<IServiceA, ServiceA>(builder =>
{
    builder.AddInterceptor<LogInterceptor>(ServiceLifetime.Transient);
    builder.AddInterceptor<TransactionalInterceptor>(ServiceLifetime.Scoped);
});
serviceCollection.AddSingleton<IFreeSql>(serviceProvider =>
{
    var logger = serviceProvider.GetRequiredService<ILogger<IFreeSql>>();
    var builder = new FreeSqlBuilder().UseConnectionString(
        connectionString:
        "data source=121.37.246.101,7433;initial catalog=acip_iplatform;user id=sa;password=sa@acip.cn;persist security info=True;packet size=4096;TrustServerCertificate=true",
        dataType: DataType.SqlServer);
        // .UseMonitorCommand(executing: _ => { },(_, s) => logger.LogInformation(s));
    return builder.Build();
});
serviceCollection.AddScoped<UnitOfWorkManager>();

var provider = serviceCollection.BuildServiceProvider();
var service = provider.GetRequiredService<ServiceA>();

var method = typeof(ServiceA).GetMethod(nameof(ServiceA.TestDbAsync));
// var @delegate = method!.CreateDelegate(typeof(Func<int, int>), service);

var stopWatch = new Stopwatch();
var number = 3;

stopWatch.Restart();
service.Test(ref number);
stopWatch.Stop();
Console.WriteLine(stopWatch.ElapsedMilliseconds);

// stopWatch.Restart();
// @delegate.DynamicInvoke(number);
// stopWatch.Stop();
// Console.WriteLine(stopWatch.ElapsedMilliseconds);

stopWatch.Restart();
var arguments = new object[] { number };
method?.Invoke(obj: service, parameters: arguments);
stopWatch.Stop();
Console.WriteLine(stopWatch.ElapsedMilliseconds);



await Task.Delay(TimeSpan.FromSeconds(3));