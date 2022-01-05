using System.Collections.Specialized;
using Infrastructure.Models;
using Quartz;
using SchedulerService;
using SchedulerService.Hubs;
using SchedulerService.Jobs;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
        builder.AddJsonFile("filesDelete.json", true, true)
            .AddJsonFile("schedule.json", true, true))
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        var filesDeleteConfiguration = configuration.GetRequiredSection("FilesDelete");
        var schedulerConfiguration = configuration.GetRequiredSection("SignalRScheduler");
        
        services.AddHostedService<Worker>();
        services.AddOptions()
            .Configure<FilesDeleteJobOptions>(filesDeleteConfiguration)
            .Configure<IEnumerable<SchedulerInfo>>(schedulerConfiguration);
        var jobOptions = filesDeleteConfiguration.Get<FilesDeleteJobOptions>();
        var schedulerOptions = schedulerConfiguration.Get<IEnumerable<SchedulerInfo>>();
        foreach (var schedulerOption in schedulerOptions)
        {
            services.AddQuartz(configurator =>
            {
                configurator.SchedulerName = schedulerOption.Name;
                configurator.UseMicrosoftDependencyInjectionJobFactory();
            });
        }
        

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
        builder.Configure(app =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapHub<ServiceHub>("services"));
        });

        builder.UseUrls("http://*:5020");
    })
    .Build();

await host.RunAsync();