using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Schedule.Clients;
using Infrastructure.Schedule.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.BackgroundServices
{
    public class SignalRScheduleWorker : BackgroundService
    {
        private readonly IScheduleClient _client;

        private readonly ILogger _logger;
        
        private readonly IOptions<ScheduleOptions> _scheduleOptions;

        public SignalRScheduleWorker(IScheduleClient client, ILoggerFactory loggerFactory, IOptions<ScheduleOptions> scheduleOptions)
        {
            _client = client;
            _scheduleOptions = scheduleOptions;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _client.StartAsync(cancellationToken);
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_client is SignalRScheduleClient signalRScheduleClient)
                await signalRScheduleClient.StopAsync(cancellationToken);
            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _client.Dispose();
            base.Dispose();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scheduleOptions = _scheduleOptions.Value;
            var createJobTasks = new List<Task>(scheduleOptions.JobOptionsMap.Count);
            foreach (var jobOptions in scheduleOptions.JobOptionsMap.Values)
            {
                var cts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
                var jobInfo = jobOptions.JobInfo;
                createJobTasks.Add(_client.CreateJobAsync(jobInfo, cts.Token));
                _logger.LogInformation("向调度服务注册[{}]信息", jobInfo.JobKey);
            }

            return Task.WhenAll(createJobTasks);
        }
    }
}