using System.Threading.Tasks;
using StackExchange.Redis;
using System;

namespace RedisSample
{
    public class LockTest : ITest
    {
        private readonly ConnectionMultiplexer _connection;

        private readonly IDatabase _db;

        private const string LockKey = "lock";

        public LockTest(ConnectionMultiplexer connection)
        {
            _connection = connection;
            _db = _connection.GetDatabase(3);
        }

        public async Task TestAsync()
        {
            const int taskCount = 3;
            var tasks = new Task[taskCount];
            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = TakeAndReleaseAsync(Guid.NewGuid().ToString(), TimeSpan.FromSeconds((i + 1) * 10));
            }
            await Task.WhenAll(tasks);
        }

        private async Task TakeAndReleaseAsync(RedisValue lockValue, TimeSpan timeout)
        {
            if (await _db.LockTakeAsync(LockKey, lockValue, timeout))
            {
                System.Console.WriteLine("{0}加锁成功", lockValue);
                // await Task.Delay(timeout);

                if (await _db.LockReleaseAsync(LockKey, lockValue.ToString()))
                {
                    System.Console.WriteLine("{0}解锁成功", lockValue);
                }
                else
                    System.Console.WriteLine("{0}解锁失败", lockValue);
            }
            else
            {
                System.Console.WriteLine("{0}加锁失败", lockValue);
            }
        }
    }
}