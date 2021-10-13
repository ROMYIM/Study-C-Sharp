using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AsynchronousTest
{
    public class InterlockedTest
    {
        [Fact]
        public void WebRequestsInterlockedTest()
        {
        //Given
            const int taskCount = 1000;
            var tasks = new Task[taskCount];

            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true
            };
            using var client = new HttpClient(httpHandler)
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
        //When
            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = Task.Run(async () => 
                {
                    await Task.Delay(500);
                    return await client.GetStringAsync("/WeatherForecast");
                });
            }
            Task.WaitAll(tasks);
        //Then
        }
    }
}