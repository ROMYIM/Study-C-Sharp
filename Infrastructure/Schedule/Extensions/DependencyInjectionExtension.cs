using System;
using Infrastructure.Schedule.BackgroundServices;
using Infrastructure.Schedule.Builders.DependencyInjection;
using Infrastructure.Schedule.Clients;
using Infrastructure.Schedule.JobExecutors;
using Infrastructure.Schedule.Logging;
using Infrastructure.Schedule.Models;
using Infrastructure.Schedule.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.Extensions
{
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// 添加调度任务的依赖注入。通过委托配置任务信息。
        /// </summary>
        /// <param name="serviceBuilder">调度服务构建者<seealso cref="ScheduleServiceBuilder"/></param>
        /// <param name="jobName">调度任务选项对应的任务名称。任务名称必须唯一。不同的调度任务需要以该名称做区分</param>
        /// <param name="buildJobInfo">配置调度任务信息的委托</param>
        /// <typeparam name="T">调度任务对应的类型参数<seealso cref="IJobExecutor"/></typeparam>
        /// <returns>调度服务构建者</returns>
        /// <exception cref="ArgumentNullException">选项对应的名称为空或者配置任务信息的委托为空</exception>
        public static ScheduleServiceBuilder AddScheduleJob<T>(this ScheduleServiceBuilder serviceBuilder, string jobName, Action<JobInfo<T>> buildJobInfo) 
            where T : class, IJobExecutor
        {
            if (buildJobInfo == null) throw new ArgumentNullException(nameof(buildJobInfo));
            if (jobName == null) throw new ArgumentNullException(nameof(jobName));
            
            var services = serviceBuilder.Services;
            services.AddOptions<JobInfo<T>>(jobName).Configure(buildJobInfo).ValidateDataAnnotations();
            services.TryAddScoped<T>();
            services.TryAddSingleton<IJobExecutor<T>, DefaultJobExecutor<T>>();
            
            var scheduleOptionsBuilder = serviceBuilder.ScheduleOptionsBuilder;
            scheduleOptionsBuilder.PostConfigure<IOptionsMonitor<JobInfo<T>>>((options, jobInfoOptions) =>
            {
                var jobInfo = jobInfoOptions.Get(jobName);
                buildJobInfo(jobInfo);
                var jobOptions = new JobOptions()
                {
                    ExecutorType = typeof(T),
                    Name = jobName,
                    JobInfo = jobInfo
                };
                options.JobOptionsMap[jobName] = jobOptions;
            });

            return serviceBuilder;
        }

        /// <summary>
        /// 添加调度任务的依赖注入。通过配置文件配置任务信息。
        /// </summary>
        /// <param name="serviceBuilder">调度服务构建者<seealso cref="ScheduleServiceBuilder"/></param>
        /// <param name="jobName">调度任务选项对应的任务名称。任务名称必须唯一。不同的调度任务需要以该名称做区分</param>
        /// <param name="configuration">配置调度任务信息的<see cref="IConfiguration"/></param>
        /// <typeparam name="T">调度任务对应的类型参数<seealso cref="IJobExecutor"/></typeparam>
        /// <returns>调度服务构建者</returns>
        /// <exception cref="ArgumentNullException">任务名称为空</exception>
        public static ScheduleServiceBuilder AddScheduleJob<T>(this ScheduleServiceBuilder serviceBuilder, string jobName, IConfiguration configuration) 
            where T : class, IJobExecutor
        {
            if (jobName == null) throw new ArgumentNullException(nameof(jobName));
            
            var services = serviceBuilder.Services;
            services.AddOptions<JobInfo<T>>(jobName).Bind(configuration).ValidateDataAnnotations();
            services.TryAddScoped<T>();
            services.TryAddSingleton<IJobExecutor<T>, DefaultJobExecutor<T>>();
            
            var scheduleOptionsBuilder = serviceBuilder.ScheduleOptionsBuilder;
            scheduleOptionsBuilder.PostConfigure<IOptionsMonitor<JobInfo<T>>>((options, jobInfoOptions) =>
            {
                var jobInfo = jobInfoOptions.Get(jobName);
                var jobOptions = new JobOptions()
                {
                    ExecutorType = typeof(T),
                    Name = jobName,
                    JobInfo = jobInfo
                };
                options.JobOptionsMap[jobName] = jobOptions;
            });
            return serviceBuilder;
        }

        /// <summary>
        /// 添加调度基础组件。必须要配置客户端的连接选项。
        /// 添加<see cref="ScheduleOptions"/><c>选项模式</c>的依赖注入。
        /// 添加<see cref="IScheduleClient"/>的依赖注入。
        /// 添加<see cref="SignalRScheduleWorker"/>的依赖注入。
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configureClientOptions">调度基础组件的选项配置</param>
        /// <returns>调度服务构建者</returns>
        /// <exception cref="ArgumentNullException">调度基础组件的选项配置委托为空</exception>
        public static ScheduleServiceBuilder AddSchedule(this IServiceCollection services, Action<SignalRClientOptions> configureClientOptions)
        {
            if (configureClientOptions == null) throw new ArgumentNullException(nameof(configureClientOptions));
            services.AddOptions<SignalRClientOptions>().Configure(configureClientOptions);
            services.TryAddSingleton<SignalRClientFactory>();
            
            var scheduleOptionsBuilder = services.AddOptions<ScheduleOptions>().Configure(options => configureClientOptions(options.SignalRClientOptions));
            services.TryAddSingleton<IScheduleClient, SignalRScheduleClient>();
            services.TryAddSingleton<SignalRScheduleWorker>();
            services.AddHostedService(s => s.GetRequiredService<SignalRScheduleWorker>());
            return new ScheduleServiceBuilder(services, scheduleOptionsBuilder);
        }

        /// <summary>
        /// 添加实时日志组件
        /// </summary>
        /// <param name="loggingBuilder">日志组件构建者</param>
        /// <param name="configureClientOptions">服务端的连接参数配置</param>
        /// <returns>日志构建者</returns>
        /// <exception cref="ArgumentNullException">服务端的连接参数配置委托</exception>
        public static ILoggingBuilder AddSchedule(this ILoggingBuilder loggingBuilder, Action<SignalRClientOptions> configureClientOptions)
        {
            if (configureClientOptions == null) throw new ArgumentNullException(nameof(configureClientOptions));
            var services = loggingBuilder.Services;
            services.AddOptions<SignalRClientOptions>().Configure(configureClientOptions);
            services.TryAddSingleton<SignalRClientFactory>();
            services.AddSingleton<ILoggingClient, SignalRLoggingClient>();
            services.TryAddSingleton<ScheduleLoggerProvider>();
            services.AddSingleton<ILoggerProvider, ScheduleLoggerProvider>(s => s.GetService<ScheduleLoggerProvider>());
            services.TryAddSingleton<SignalRLoggingWorker>();
            services.AddHostedService(s => s.GetService<SignalRLoggingWorker>());
            return loggingBuilder;
        }
    }
}