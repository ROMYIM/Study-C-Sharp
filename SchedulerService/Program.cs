using Infrastructure.Converters;
using Infrastructure.Extensions.DependencyInjection;
using Infrastructure.Filters;
using Infrastructure.Models;
using Microsoft.AspNetCore.HttpLogging;
using Quartz;
using SchedulerService;
using SchedulerService.Hubs;
using SchedulerService.Query;
using SchedulerService.Repositories;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ServiceHostRepository>();
        services.AddSingleton<ServiceHostQueryService>();
        services.AddSingleton<SchedulerWorker>();
        services.AddHostedService(s => s.GetRequiredService<SchedulerWorker>());
        services.AddQuartz(configurator =>
        {
            configurator.SchedulerName = SchedulerHub.SchedulerName;
            configurator.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddSignalR(options =>
        {
            options.KeepAliveInterval = TimeSpan.FromSeconds(10);
            options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
            options.HandshakeTimeout = TimeSpan.FromSeconds(10);
            options.EnableDetailedErrors = true;
        });
    })
    .ConfigureWebHostDefaults(builder =>
    {
        builder.ConfigureServices(services =>
        {
            services.AddStringBuilderPool();
            services.AddControllers(options =>
            {
                // options.Filters.Add<ActionResultFilter<TraceApiResult>>();
                options.Filters.Add<ActionExceptionFilter<TraceApiResult>>();
            }).AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter()));
            services.AddHttpLogging(options =>
            {
                options.LoggingFields |= HttpLoggingFields.RequestBody;
                options.LoggingFields |= HttpLoggingFields.ResponseBody;
            });
        });
        builder.Configure(app =>
        {
            app.UseRouting();
            app.UseHttpLogging();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SchedulerHub>("Schedule");
                endpoints.MapHub<LoggingHub>("Logging");
            });
        });

        builder.UseUrls("http://*:5020");
    })
    .Build();

host.Run();