using System;
using Xunit;
using System.Threading.Tasks;
using System.Threading;

namespace AsynchronousTest
{
    public class AsyncAwaitTest
    {
        [Fact]
        public async Task TaskDelayTest()
        {
            var beforeThread = Thread.CurrentThread.ManagedThreadId;

            await Task.Delay(1000);

            var afterThread = Thread.CurrentThread.ManagedThreadId;

            Assert.Equal(beforeThread, afterThread);
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
