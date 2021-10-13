using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RedisSample
{
    public class PubSubTest
    {
        private readonly ConnectionMultiplexer _connection;

        private readonly ISubscriber _subscriber;

        public PubSubTest(ConnectionMultiplexer connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _subscriber = connection.GetSubscriber();

            _subscriber.UnsubscribeAll();

            _subscriber.Subscribe(new RedisChannel("zikey.*", RedisChannel.PatternMode.Auto), (channel, message) =>
            {
                Console.WriteLine("channel:{0}; message:{1}; channel pattern:{2}", channel, message, "zikey.*");
            });

            _subscriber.Subscribe(new RedisChannel("zikey.1", RedisChannel.PatternMode.Auto), (channel, message) =>
            {
                Console.WriteLine("channel:{0}; message:{1}; channel pattern:{2}", channel, message, "zikey.1");
            });
        }

        public async Task TestAsync()
        {
            await _subscriber.PublishAsync("zikey.3", "3");
        }

    }
}