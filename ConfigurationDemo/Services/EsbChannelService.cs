using ConfigurationDemo.Infranstructures.Models;
using Microsoft.Extensions.Options;

namespace ConfigurationDemo.Services
{
    public class EsbChannelService : IChannelService<EsbOptions>
    {
        private readonly IOptionsMonitor<EsbOptions> _optionsMonitor;

        public string PostType => "esb";

        public EsbOptions Options => _optionsMonitor.Get(PostType);

        public EsbChannelService(IOptionsMonitor<EsbOptions> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
        }
    }
}