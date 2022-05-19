using System;
using System.IO;
using Xunit;
using System.Threading.Tasks;
using System.Threading;
using Xunit.Abstractions;

namespace AsynchronousTest
{
    public class AsyncAwaitTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public AsyncAwaitTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task TaskDelayTest()
        {
            var beforeThread = Thread.CurrentThread.ManagedThreadId;

            await Task.Delay(1000);

            var afterThread = Thread.CurrentThread.ManagedThreadId;

            Assert.NotEqual(beforeThread, afterThread);
        }

        [Fact]
        public async Task ReadFileAsyncTest()
        {
            _testOutputHelper.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
            var task1 = File.ReadAllBytesAsync("file_list20220328151827.zip").ContinueWith((task, o) =>
            {
                // _testOutputHelper.WriteLine("{0}, {1}",o, DateTime.Now.Ticks);
            }, "file_list20220328151827.zip");
            task1.ConfigureAwait(false).GetAwaiter().OnCompleted(() => _testOutputHelper.WriteLine("file_list20220328151827.zip - continued, {0}", DateTime.Now.Ticks));
            
            var task2 = File.ReadAllBytesAsync("电子申请回执 (2).zip").ContinueWith((task, o) =>
            {
                // _testOutputHelper.WriteLine("{0}, {1}",o, DateTime.Now.Ticks);
            }, "电子申请回执 (2).zip");
            task1.ConfigureAwait(false).GetAwaiter().OnCompleted(() => _testOutputHelper.WriteLine("电子申请回执 (2).zip, {0} - continued", DateTime.Now.Ticks));
            _testOutputHelper.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
            
            await task1;
            await Task.Delay(TimeSpan.FromSeconds(10));
            _testOutputHelper.WriteLine("等待");
            await task2;
        }

        [Fact]
        public void TaskDelayAsyncTest()
        {
            //Given
            var beforeThread = Thread.CurrentThread.ManagedThreadId;
            //When
            Task.Delay(1000);
            var afterThread = Thread.CurrentThread.ManagedThreadId;
            //Then
            Assert.Equal(beforeThread, afterThread);
        }

        [Fact]
        public async Task TaskFromResultTest()
        {
            var beforeThreadId = Thread.CurrentThread.ManagedThreadId;

            await Task.CompletedTask;

            var afterThread = Thread.CurrentThread.ManagedThreadId;

            Assert.Equal(beforeThreadId, afterThread);
        }
    }
}
