using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NettyDemo.Infrastructure.Caches.Abbractions;
using NettyDemo.Models.Dtos;

namespace NettyDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IKeyValueCache<string, IChannel> _channelCache;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IKeyValueCache<string, IChannel> channelCache)
        {
            _logger = logger;
            _channelCache = channelCache;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("message")]
        public async Task<Options> PushMessage()
        {
            var options = new Options
            {
                Id = Guid.NewGuid().ToString(),
                Type = "PostType"
            };

            var channels = _channelCache.Values;
            foreach (var channel in channels)
            {
                await channel.WriteAndFlushAsync(options);
            }

            return options;
        }

        [Route("host")]
        public IEnumerable<string> ChannelHosts()
        {
            var hosts = _channelCache.Keys;
            return hosts;
        }
    }
}
