using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisSample
{
    class Program
    {
        private const string ConnectionString = "172.23.234.16:6379";

        private const int Database = 0;

        static async Task Main(string[] args)
        {
            using var redisConnection = await ConnectionMultiplexer.ConnectAsync(ConnectionString);
            var database = redisConnection.GetDatabase(Database);

            var test = new LuaTest(redisConnection);
            await test.TestAsync();
        }
    }
}
