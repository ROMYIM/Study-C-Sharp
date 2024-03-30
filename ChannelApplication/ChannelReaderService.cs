using System.Threading.Channels;

namespace ChannelApplication;

public class ChannelReaderService : BackgroundService
{
    private readonly Channel<string> _channel;

    private readonly ILogger<ChannelReaderService> _logger;

    public ChannelReaderService(Channel<string> channel, ILogger<ChannelReaderService> logger)
    {
        _channel = channel;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var str in _channel.Reader.ReadAllAsync(stoppingToken))
        {
            _logger.LogInformation("consume {Str}", str);
            await Task.Delay(5000, stoppingToken);
        }
        
        _logger.LogInformation("consume completed");
    }

    /// <inheritdoc />
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("reader service stop");
        return base.StopAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override void Dispose()
    {
        _logger.LogInformation("Dispose");
        base.Dispose();
    }
}