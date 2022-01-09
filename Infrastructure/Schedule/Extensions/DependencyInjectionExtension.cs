using System;
using Infrastructure.Models;
using Infrastructure.Schedule.BackgroundServices;
using Infrastructure.Schedule.Clients;
using Infrastructure.Schedule.JobExecutors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.Schedule.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddScheduleJob<T>(this IServiceCollection services, Action<JobInfo> jobInfoBuilder) 
            where T : class, IJobExecutor
        {
            if (jobInfoBuilder == null) throw new ArgumentNullException(nameof(jobInfoBuilder));
            services.AddOptions<JobInfo>().Configure(jobInfoBuilder).ValidateDataAnnotations();
            return services.AddScheduleJob<T>();
        }

        public static IServiceCollection AddScheduleJob<T>(this IServiceCollection services)
            where T : class, IJobExecutor
        {
            services.TryAddSingleton<DefaultJobExecutor<T>>();
            services.TryAddSingleton<IExceptionHandleJobExecutor<T>>(s => s.GetRequiredService<DefaultJobExecutor<T>>());
            services.AddSingleton<IExceptionHandleJobExecutor, DefaultJobExecutor<T>>(s =>
                s.GetRequiredService<DefaultJobExecutor<T>>());

            services.TryAddSingleton<SignalRScheduleClient>();
            services.TryAddSingleton<IScheduleClient>(s =>
            {
                var client = s.GetRequiredService<SignalRScheduleClient>();
                var jobExecutor = s.GetRequiredService<IExceptionHandleJobExecutor<T>>();
                client.RegisterJobExecutor(jobExecutor);
                return client;
            });
            
            services.TryAddSingleton<SignalRScheduleWorker>();
            services.AddHostedService(s => s.GetRequiredService<SignalRScheduleWorker>());
            
            services.TryAddScoped<T>();
            return services;
        }
        
        public static IServiceCollection AddScheduleJob<T>(this IServiceCollection services, IConfiguration configuration) 
            where T : class, IJobExecutor
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            services.AddOptions<JobInfo>().Bind(configuration).ValidateDataAnnotations();
            return services.AddScheduleJob<T>();
        }
    }
}