using System.Threading.Tasks;
using Infrastructure.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.Clients
{
    public class SignalRScheduleClient : IScheduleClient
    {
        private readonly ILogger _logger;

        private readonly IOptions<JobInfo> _options;

        private readonly HubConnection _connection;

        public SignalRScheduleClient(ILoggerFactory loggerFactory, IOptions<JobInfo> options)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _options = options;
            
            var jobInfoOptions = options.Value;
            _connection = new HubConnectionBuilder()
                .WithUrl(jobInfoOptions.Host)
                .WithAutomaticReconnect()
                .Build();
            
        }

        public virtual async Task CreateJobAsync(JobInfo jobInfo)
        {
            if (_connection.State == HubConnectionState.Disconnected)
            {
                await _connection.StartAsync();
                _logger.LogInformation("连接成功");
            }
            
            jobInfo ??= _options.Value;
            await _connection.SendAsync(nameof(CreateJobAsync), jobInfo);
            _logger.LogInformation("创建任务成功");
        }
        
    }
}