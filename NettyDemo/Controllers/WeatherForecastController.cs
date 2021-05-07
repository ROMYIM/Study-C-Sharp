using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NettyDemo.Infrastructure.Caches.Abbractions;
using NettyDemo.Models.Dtos;
using Zaabee.RabbitMQ.Abstractions;

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

        private readonly IZaabeeRabbitMqClient _mqClient;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, 
            IKeyValueCache<string, IChannel> channelCache, 
            IZaabeeRabbitMqClient mqClient)
        {
            _logger = logger;
            _channelCache = channelCache;
            _mqClient = mqClient;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var requstId = HttpContext.TraceIdentifier;
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        [Route("message")]
        public async Task<Options> PushMessage([FromQuery] string code, [FromBody]Options options)
        {

            var channels = _channelCache.Values;
            // foreach (var channel in channels)
            // {
            //     await channel.WriteAndFlushAsync(options);
            // }

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
