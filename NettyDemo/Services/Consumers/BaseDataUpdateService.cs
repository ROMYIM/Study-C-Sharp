using System;
using System.Linq;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using NettyDemo.Infrastructure.Caches.Abbractions;
using NettyDemo.Models.Dtos;
using NettyDemo.Models.MessageCommads;

namespace NettyDemo.Services.Consumers
{
    public class BaseDataUpdateService
    {
        private readonly IKeyValueCache<string, IChannel> _channelCache;

        private readonly ILogger _logger;

        public BaseDataUpdateService(ILoggerFactory loggerFactory, IKeyValueCache<string, IChannel> channelCache)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _channelCache = channelCache;
        }

        public void Handle(BaseDataUpdateCommand command)
        {
            command.TriggerTime = DateTimeOffset.Now;

            var channels = _channelCache.Values;
            var tasks = new Task[channels.Count];

            var i = 0;
            foreach (var channel in channels)
            {
                var  updateData = new BaseUpdateData();
                updateData.Data.AddRange(command.UpdateContents.Select(c =>
                new Options
                {
                    Id = c.DataKey,
                    Type = c.DataType
                }).ToList());

                tasks[i++] = channel.WriteAndFlushAsync(updateData);
            }

            Task.WaitAll(tasks);
        }
    }
}