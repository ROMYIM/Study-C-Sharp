using Quartz;
using SchedulerService;
using SchedulerService.Hubs;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
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
        builder.ConfigureServices(services => services.AddControllers());
        
        builder.Configure(app =>
        {
            app.UseRouting();
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