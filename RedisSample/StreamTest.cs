using StackExchange.Redis;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace RedisSample
{
    public class StreamTest : ITest
    {
        private readonly IDatabase _db;

        private const string ZikeyStream = "ZikeyStream-2";

        private const string ZikeyGroup = "ZikeyGroup";

        public StreamTest(ConnectionMultiplexer connection)
        {
            _db = connection.GetDatabase(1);
            Task.Factory.StartNew(async () =>
            {
                await CreateConsumerGroupAsync();
                await ConsumerGroupConusmeAsync();
            });
        }

        public async ValueTask<bool> CreateConsumerGroupAsync()
        {
            await _db.StreamDeleteConsumerGroupAsync(ZikeyStream,  ZikeyGroup);
            return await _db.StreamCreateConsumerGroupAsync(ZikeyStream, ZikeyGroup, StreamPosition.NewMessages);
        }

        public async Task QueryConsumerGroupInfoAsync()
        {
            var groupInfo = await _db.StreamGroupInfoAsync(ZikeyStream);
            System.Console.WriteLine(JsonSerializer.Serialize(groupInfo, new JsonSerializerOptions
            {
                WriteIndented = true
            }));
        }

        public async Task PublishMessageToStreamAsync(string messageKey)
        {
            var messages = new NameValueEntry[1];
            for (int i = 0; i < 1; i++)
            {
                messages[i] = new NameValueEntry(messageKey, i);
            }
            var messageId = await _db.StreamAddAsync(ZikeyStream, messages);
            System.Console.WriteLine("messageId:{0}", messageId);

            
        }

        public async Task ConsumerMessageFromStreamAsync()
        {
            // var script = @"redis.call('xread', 'streams', @key, @position)";
            // var prepared = LuaScript.Prepare(script);
            // var result = await _db.ScriptEvaluateAsync(prepared, new 
            // {
            //     key = ZikeyStream,
            //     position = "0-0"
            // });

            

            var messageInfo = await _db.StreamReadAsync(ZikeyStream, position: StreamPosition.Beginning);
            System.Console.WriteLine(JsonSerializer.Serialize(messageInfo.Select(m => new StreamMessage(m)).ToArray(), new JsonSerializerOptions
            {
                WriteIndented = true
            }));
        }

        public async Task ConsumerGroupConusmeAsync()
        {
            while (true)
            {
                // var streamPosition = StreamPosition.Beginning;

                // var groups = await _db.StreamGroupInfoAsync(ZikeyStream);
                // if (groups?.Any(g => g.Name == ZikeyGroup) ?? false)
                // {
                //     var groupInfo = groups.Where(g => g.Name == ZikeyGroup).FirstOrDefault();
                //     streamPosition = groupInfo.LastDeliveredId;  
                //     System.Console.WriteLine(streamPosition);      
                // }
                

                var messageInfo = await _db.StreamReadGroupAsync(ZikeyStream, ZikeyGroup, consumerName:"yim", position: StreamPosition.NewMessages);
                if (messageInfo?.Any() ?? false)
                {
                    foreach (var info in messageInfo)
                    {
                        System.Console.WriteLine(JsonSerializer.Serialize(new StreamMessage(info), new JsonSerializerOptions
                        {
                            WriteIndented = true
                        }));
                        await _db.StreamAcknowledgeAsync(ZikeyStream, ZikeyGroup, info.Id);
                    }
                }
            }
        }

        public async Task TestAsync()
        {
            var timeSpan = TimeSpan.FromSeconds(500);

            // await PublishMessageToStreamAsync("gin");

            // await Task.Delay(timeSpan);


            // await PublishMessageToStreamAsync("yim");

            // await Task.Delay(timeSpan);

            await PublishMessageToStreamAsync("zikey");

            await Task.Delay(timeSpan);

            // if (await CreateConsumerGroupAsync())
            // {
            //     await QueryConusmerGroupInfoAsync();
            //     await ConsumerGroupConusmeAsync();
            //     await QueryConusmerGroupInfoAsync();
            // }

            // await ConsumerMessageFromStreamAsync();
        }

        public struct StreamMessage
        {
            public string MessageId { get; }

            public KeyValuePair<string, string>[] Content { get; }

            public StreamMessage(StreamEntry entry)
            {
                MessageId  = entry.Id;
                Content = entry.Values.Select(e => new KeyValuePair<string, string>(e.Name, e.Value)).ToArray();
            }
        }
    }
}