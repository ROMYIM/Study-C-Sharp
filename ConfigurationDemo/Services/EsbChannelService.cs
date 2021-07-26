using ConfigurationDemo.Infranstructures.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ConfigurationDemo.Services
{
    public class EsbChannelService : IChannelService<EsbOptions>
    {
        private readonly IOptionsMonitor<EsbOptions> _optionsMonitor;

        private readonly IConfiguration _configuration;

        public string PostType => "esb";

        public EsbOptions Options => _configuration.GetSection(PostType).Get<EsbOptions>();

        public EsbChannelService(IOptionsMonitor<EsbOptions> optionsMonitor, IConfiguration configuration)
        {
            _optionsMonitor = optionsMonitor;
            _configuration = configuration;
        }
    }
}