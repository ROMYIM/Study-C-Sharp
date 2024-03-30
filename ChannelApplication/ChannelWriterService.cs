using System.Threading.Channels;

namespace ChannelApplication;

public class ChannelWriterService : BackgroundService
{
    private readonly Channel<string> _channel;

    public ChannelWriterService(Channel<string> channel)
    {
        _channel = channel;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(5000, stoppingToken);
        foreach (var i in Enumerable.Range(1, 10))
        {
            await _channel.Writer.WriteAsync(i.ToString(), stoppingToken);
        }
    }

    /// <inheritdoc />
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Writer.Complete();
        return base.StopAsync(cancellationToken);
    }
}