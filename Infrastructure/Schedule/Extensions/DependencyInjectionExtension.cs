﻿using System;
using Infrastructure.Models;
using Infrastructure.Schedule.BackgroundServices;
using Infrastructure.Schedule.Builders.DependencyInjection;
using Infrastructure.Schedule.Clients;
using Infrastructure.Schedule.JobExecutors;
using Infrastructure.Schedule.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.Schedule.Extensions
{
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// 添加调度任务的依赖注入。通过委托配置任务信息。
        /// </summary>
        /// <param name="serviceBuilder">调度服务构建者<seealso cref="ScheduleServiceBuilder"/></param>
        /// <param name="jobName">调度任务选项对应的任务名称。任务名称必须唯一。不同的调度任务需要以该名称做区分</param>
        /// <param name="jobInfoBuilder">配置调度任务信息的委托</param>
        /// <typeparam name="T">调度任务对应的类型参数<seealso cref="IJobExecutor"/></typeparam>
        /// <returns>调度服务构建者</returns>
        /// <exception cref="ArgumentNullException">选项对应的名称为空或者配置任务信息的委托为空</exception>
        public static ScheduleServiceBuilder AddScheduleJob<T>(this ScheduleServiceBuilder serviceBuilder, string jobName, Action<JobInfo> jobInfoBuilder) 
            where T : class, IJobExecutor
        {
            if (jobInfoBuilder == null) throw new ArgumentNullException(nameof(jobInfoBuilder));
            if (jobName == null) throw new ArgumentNullException(nameof(jobName));
            
            var services = serviceBuilder.Services;
            services.AddOptions<JobInfo>(jobName).Configure(jobInfoBuilder).ValidateDataAnnotations();
            services.TryAddScoped<T>();
            services.TryAddSingleton<IJobExecutor<T>, DefaultJobExecutor<T>>();
            
            var optionsBuilder = serviceBuilder.OptionsBuilder;
            optionsBuilder.PostConfigure(options =>
            {
                var jobOptions = new JobOptions()
                {
                    ExecutorType = typeof(T),
                    Name = jobName
                };
                options.JobOptionsList.Add(jobOptions);
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
            services.AddOptions<JobInfo>(jobName).Bind(configuration).ValidateDataAnnotations();
            services.TryAddScoped<T>();
            services.TryAddSingleton<IJobExecutor<T>, DefaultJobExecutor<T>>();
            
            var optionsBuilder = serviceBuilder.OptionsBuilder;
            optionsBuilder.PostConfigure(options =>
            {
                var jobOptions = new JobOptions()
                {
                    ExecutorType = typeof(T),
                    Name = jobName
                };
                options.JobOptionsList.Add(jobOptions);
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
        /// <param name="configureScheduleOptions">调度基础组件的选项配置</param>
        /// <returns>调度服务构建者</returns>
        /// <exception cref="ArgumentNullException">调度基础组件的选项配置委托为空</exception>
        public static ScheduleServiceBuilder AddSchedule(this IServiceCollection services, Action<ScheduleOptions> configureScheduleOptions)
        {
            if (configureScheduleOptions == null) throw new ArgumentNullException(nameof(configureScheduleOptions));
            var optionsBuilder = services.AddOptions<ScheduleOptions>().Configure(configureScheduleOptions);
            services.AddOptions<JobInfo>();
            services.TryAddSingleton<IScheduleClient, SignalRScheduleClient>();
            services.TryAddSingleton<SignalRScheduleWorker>();
            services.AddHostedService(s => s.GetRequiredService<SignalRScheduleWorker>());
            return new ScheduleServiceBuilder(services, optionsBuilder);
        }
    }
}