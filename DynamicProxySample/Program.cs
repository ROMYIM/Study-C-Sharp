// See https://aka.ms/new-console-template for more information

using DynamicProxy.Extensions;
using DynamicProxySample.Interceptors;
using DynamicProxySample.Interfaces;
using DynamicProxySample.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

var serviceCollection = new ServiceCollection();
serviceCollection.AddLogging(builder => builder.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Information;
}));
serviceCollection.AddServiceProxy<IServiceA, ServiceA>(builder => builder.AddInterceptor<LogInterceptor>(ServiceLifetime.Transient));

var provider = serviceCollection.BuildServiceProvider();
var service = provider.GetService<IServiceA>();
service?.Test(7);