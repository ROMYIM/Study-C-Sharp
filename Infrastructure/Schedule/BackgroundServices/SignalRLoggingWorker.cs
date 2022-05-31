using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Schedule.Clients;
using Infrastructure.Schedule.Logging;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Schedule.BackgroundServices
{
    public class SignalRLoggingWorker : BackgroundService
    {
        private readonly ILoggingClient _client;

        private readonly ScheduleLoggerProvider _loggerProvider;

        public SignalRLoggingWorker(ILoggingClient client, ScheduleLoggerProvider loggerProvider)
        {
            _client = client;
            _loggerProvider = loggerProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(async (provider) =>
            {
                if (provider is ScheduleLoggerProvider loggerProvider)
                {
                    while (true)
                    {
                        if (!loggerProvider.IsEnabled) continue;
                        await foreach (var logInfo in _loggerProvider.TakeLogInfosAsync().WithCancellation(stoppingToken))
                        {
                            await _client.PostLogsAsync(stoppingToken, logInfo);
                        }
                    }
                }
            }, _loggerProvider, TaskCreationOptions.LongRunning);
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _client.StartAsync(cancellationToken);
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.StopAsync(cancellationToken);
            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _client.DisposeAsync().GetAwaiter().GetResult();
            _loggerProvider.Dispose();
        }
    }
}