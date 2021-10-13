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
                var script = @"local currentTotalCount = 0
                            local countArray = redis.call('hvals', @userCountKey)
                            for index,value in ipairs(countArray) do
                                currentTotalCount = currentTotalCount + tonumber(value)
                            end
                            if (currentTotalCount >= tonumber(@totalCount)) then
                                return 0
                            else
                                local userCount = redis.call('hget', @userCountKey, @userKey)
                                if (tonumber(userCount) >= tonumber(@userPerTotal)) then
                                    return 0
                                else
                                    userCount = userCount + 1
                                    redis.call('hset', @userCountKey, @userKey, userCount)
                                    return 1
                                end
                            end";
                var prepared = LuaScript.Prepare(script);
                var result =  await _db.ScriptEvaluateAsync(prepared, new 
                {
                    userCountKey = "UsersCount",
                    totalCount = 10,
                    userKey = "Zikey",
                    userPerTotal = 3
                });

                System.Console.WriteLine(result);
                scriptResult = (int)result;
            } while (scriptResult == 1);
        }
    }
}