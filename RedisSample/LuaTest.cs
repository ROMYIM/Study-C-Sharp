using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisSample
{
    public class LuaTest : ITest
    {
        private readonly IDatabase _db;

        public LuaTest(ConnectionMultiplexer connection)
        {
            _db = connection.GetDatabase(2);
        }

        public async Task TestAsync()
        {
            int scriptResult;
            do
            {
                var script = @"local currentTotalCount = redis.call('hget', @userCountKey, @totalKey)
                            if (not(currentTotalCount)) then
                                currentTotalCount = 0
                            else
                                currentTotalCount = tonumber(currentTotalCount)
                            end

                            if (currentTotalCount + 1 >= tonumber(@totalCount)) then
                                return 0
                            else
                                local userCount = redis.call('hget', @userCountKey, @userKey)
                                if (not(userCount)) then
                                    userCount = 0
                                else
                                    userCount = tonumber(userCount)
                                end

                                if (userCount >= tonumber(@userPerTotal)) then
                                    return 0
                                else
                                    userCount = userCount + 1
                                    currentTotalCount = currentTotalCount + 1
                                    redis.call('hset', @userCountKey, @userKey, userCount)
                                    redis.call('hset', @userCountKey, @totalKey, currentTotalCount)
                                    return 1
                                end
                            end";
                var prepared = LuaScript.Prepare(script);
                var result =  await _db.ScriptEvaluateAsync(prepared, new 
                {
                    userCountKey = "UsersCount",
                    totalCount = 10,
                    userKey = "Zikey",
                    totalKey = "Total",
                    userPerTotal = 3
                });

                System.Console.WriteLine(result);
                scriptResult = (int)result;
            } while (scriptResult == 1);
        }
    }
}