using System.Collections.Specialized;
using Infrastructure.Models;
using Quartz;
using SchedulerService;
using SchedulerService.Jobs;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
        builder.AddJsonFile("filesDelete.json", true, true))
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        var filesDeleteConfiguration = configuration.GetRequiredSection("FilesDelete");
        
        services.AddHostedService<Worker>();
        services.AddOptions().Configure<FilesDeleteJobOptions>(filesDeleteConfiguration);
        var jobOptions = filesDeleteConfiguration.Get<FilesDeleteJobOptions>();
        services.AddQuartz(new NameValueCollection()
        {
            { Worker.CronExpressionKey(jobOptions.JobKey), jobOptions.CronExpression }
        },configurator =>
        {
            configurator.UseMicrosoftDependencyInjectionJobFactory();

            configurator.AddJob<FilesDeleteJob>(jobConfigurator =>
            {
                jobConfigurator.WithIdentity(JobKey.Create(jobOptions.JobKey));
                jobConfigurator.WithDescription(jobOptions.Description);
            }).AddTrigger(triggerConfigurator =>
            {
                triggerConfigurator.ForJob(JobKey.Create(jobOptions.JobKey));
                triggerConfigurator.WithIdentity(jobOptions.JobKey);
                triggerConfigurator.WithCronSchedule(jobOptions.CronExpression);
            });
        });
    })
    .Build();

await host.RunAsync();