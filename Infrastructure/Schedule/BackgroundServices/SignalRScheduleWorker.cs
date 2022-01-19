using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Schedule.Clients;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.BackgroundServices
{
    public class SignalRScheduleWorker : BackgroundService
    {
        private readonly IScheduleClient _client;

        private readonly ILogger _logger;

        private readonly IOptionsMonitor<JobInfo> _options;

        public SignalRScheduleWorker(IScheduleClient client, ILoggerFactory loggerFactory, IOptionsMonitor<JobInfo> options)
        {
            _client = client;
            _options = options;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            if (_client is SignalRScheduleClient signalRScheduleClient)
            {

                await signalRScheduleClient.StartAsync(cancellationToken);
            }
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var jobInfo = _options.Value;
            await _client.CreateJobAsync(jobInfo, stoppingToken);
            _logger.LogInformation("向调度服务注册任务信息");
        }
    }
}