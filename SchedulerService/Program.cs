using Quartz;
using SchedulerService;
using SchedulerService.Hubs;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<SchedulerWorker>();
        services.AddHostedService<SchedulerWorker>(s => s.GetRequiredService<SchedulerWorker>());
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
        builder.Configure(app =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapHub<SchedulerHub>("signalr"));
        });

        builder.UseUrls("http://*:5020");
    })
    .Build();

await host.RunAsync();