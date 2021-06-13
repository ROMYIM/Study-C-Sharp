namespace ConfigurationDemo.Services
{
    public interface IChannelService<TOptions>
    {
        string PostType { get; }

        TOptions Options { get; }
    }
}