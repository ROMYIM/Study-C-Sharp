// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConsoleApp1
{ 
    class Program 
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            // Task.Run(async () =>
            // {
            //     await ChannelRun(5000,0, 2, 50, 5);
            // }, cts.Token); 
            Console.WriteLine("运行开始...");
            Console.WriteLine(token.IsCancellationRequested);
            cts.Cancel();
            Console.WriteLine(token.IsCancellationRequested);
            cts.TryReset();
            Console.WriteLine(token.IsCancellationRequested);
            Console.WriteLine("运行结束..."); 
            Console.ReadLine();
        } 
        
        /// <summary>
        /// channel运行
        /// </summary>
        /// <param name="readDelayMs">读取器每次读取完等待时间</param>
        /// <param name="writeDelayMs">写入器每次写入完等待时间</param>
        /// <param name="finalNumberOfReaders">几个读取器同时读取</param>
        /// <param name="howManyMessages">写入器总共写入多少消息</param>
        /// <param name="maxCapacity">channel最大容量</param>
        /// <returns></returns>
        public static async Task ChannelRun(int readDelayMs, int writeDelayMs, int finalNumberOfReaders,int howManyMessages, int maxCapacity ) 
        { 
            // 创建channel
            var channel = Channel.CreateBounded<string>(maxCapacity); 
            var reader = channel.Reader; 
            var writer = channel.Writer; 
            var tasks = new List<Task>(); 
            // 读取器执行读取任务，可以设置多个读取器同时读取
            for (var i = 0; i < finalNumberOfReaders; i++)
            {
                var idx = i; 
                tasks.Add(Task.Run(() => Read(reader, idx + 1,readDelayMs)));
            } 
            // 写入器执行写入操作
            for (var i = 0; i < howManyMessages; i++) 
            { 
                Console.WriteLine($"写入器在{DateTime.Now.ToLongTimeString()}写入：{i}"); 
                await writer.WriteAsync($"发布消息：'{i}"); 
                // 写入完等待片刻
                await Task.Delay(writeDelayMs); 
            } 
            // 写入器标记完成状态
            writer.Complete(); 
            // 等待读取器读取完成
            await reader.Completion; 
            // 等待读取器所有的Task完成
            await Task.WhenAll(tasks); 
        } 
        /// <summary>
        /// 读取数据任务
        /// </summary>
        /// <param name="theReader">读取器</param>
        /// <param name="readerNumber">读取器编号</param>
        /// <param name="delayMs">读取完等待时间</param>
        /// <returns>任务</returns>
        public static async Task Read(ChannelReader<string> theReader, int readerNumber, int delayMs) 
        { 
            // 循环判断读取器是否完成状态
            while (await theReader.WaitToReadAsync()) 
            { 
                // 尝试读取数据
                while (theReader.TryRead(out var theMessage)) 
                { 
                    Console.WriteLine($"线程{readerNumber}号读取器在{DateTime.Now.ToLongTimeString()}读取到了消息：'{theMessage}'"); 
                    // 读取完等待片刻
                    await Task.Delay(delayMs); 
                } 
            } 
        } 
    }
    
}