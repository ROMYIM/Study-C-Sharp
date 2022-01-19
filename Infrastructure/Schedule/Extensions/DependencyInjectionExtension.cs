using System;
using Infrastructure.Schedule.BackgroundServices;
using Infrastructure.Schedule.Clients;
using Infrastructure.Schedule.JobExecutors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.Extensions
{
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// 客户端接入调度系统的依赖注入
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="jobName">任务的配置名称</param>
        /// <param name="jobInfoBuilder">客户端任务信息的选项配置委托</param>
        /// <typeparam name="T">客户端执行任务逻辑的服务类型。实现<see cref="IJobExecutor"/>接口</typeparam>
        /// <returns>服务集合</returns>
        /// <exception cref="ArgumentNullException">服务集合为空或者任务信息的选项配置为空</exception>
        public static IServiceCollection AddScheduleJob<T>(this IServiceCollection services, string jobName, Action<JobInfo> jobInfoBuilder) 
            where T : class, IJobExecutor
        {
            if (jobInfoBuilder == null) throw new ArgumentNullException(nameof(jobInfoBuilder));
            if (jobName == null) throw new ArgumentNullException(nameof(jobName));
            services.AddOptions<JobInfo>(jobName).Configure(jobInfoBuilder).ValidateDataAnnotations();
            return services.AddScheduleJob<T>(jobName);
        }

        private static IServiceCollection AddScheduleJob<T>(this IServiceCollection services, string jobName)
            where T : class, IJobExecutor
        {
            services.TryAddSingleton<SignalRScheduleClient>();
            services.TryAddSingleton<IScheduleClient>(s =>
            {
                var client = s.GetRequiredService<SignalRScheduleClient>();
                var options = s.GetRequiredService<IOptionsMonitor<JobInfo>>();
                var jobInfo = options.Get(jobName);
                client.RegisterJobExecutor<T>(jobInfo);
                return client;
            });
            services.TryAddSingleton<SignalRScheduleWorker>();
            services.AddHostedService(s => s.GetRequiredService<SignalRScheduleWorker>());
            services.TryAddScoped<T>();
            return services;
        }

        /// <summary>
        /// 客户端接入调度系统的依赖注入
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="jobName">任务的配置名称</param>
        /// <param name="configuration">客户端任务信息的选项配置</param>
        /// <typeparam name="T">客户端执行任务逻辑的服务类型。实现<see cref="IJobExecutor"/>接口</typeparam>
        /// <returns>服务集合</returns>
        /// <exception cref="ArgumentNullException">服务集合为空或者任务信息的选项配置为空</exception>
        public static IServiceCollection AddScheduleJob<T>(this IServiceCollection services, string jobName, IConfiguration configuration) 
            where T : class, IJobExecutor
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            services.AddOptions<JobInfo>(jobName).Bind(configuration).ValidateDataAnnotations();
            return services.AddScheduleJob<T>(jobName);
        }
    }
}