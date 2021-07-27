using ConfigurationDemo.Infranstructures.Models;
using ConfigurationDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EsbChannelController : ControllerBase
    {
        private readonly IChannelService<EsbOptions> _channelService;

        public EsbChannelController(IChannelService<EsbOptions> channelService)
        {
            _channelService = channelService;
        }

        [Route("[action]")]
        [HttpGet]
        public EsbOptions Options()
        {
            return _channelService.Options;
        }
    }
}