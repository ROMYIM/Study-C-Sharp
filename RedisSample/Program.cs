using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisSample
{
    class Program
    {
        private const string ConnectionString = "127.0.0.1:6379";

        private const int Database = 0;

        static async Task Main(string[] args)
        {

            var options = new ConfigurationOptions
            {
                Password = "123456",
            };

            options.EndPoints.Add(ConnectionString);

            using var redisConnection = await ConnectionMultiplexer.ConnectAsync(options);
            var database = redisConnection.GetDatabase(Database);

            var test = new LockTest(redisConnection);
            await test.TestAsync();
        }
    }
}
