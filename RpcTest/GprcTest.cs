using System.Threading.Tasks;
using Grpc.Net.Client;
using Xunit;

namespace RpcTest
{
    public class GrpcTest
    {
        private const string LocalHttpsHost = "https://localhost:5001";

        [Fact]
        public async Task GreeterSayHelloTest()
        {
            var request = new Greet.HelloRequest
            {
                Name = "YIM",
            };

            using var channel = GrpcChannel.ForAddress(LocalHttpsHost);
            var client = new Greet.Greeter.GreeterClient(channel);
            var helloReply = await client.SayHelloAsync(request);

            Assert.Equal("Hello " + request.Name, helloReply.Message);
        }

        [Fact]
        public async Task GreeterGoodByeTest()
        {
        //Given
            var request = new Greet.GoodByeRequest
            {
                Name = "yim",
                Message = "Good bye!  You are so handsome"
            };
        //When
            using var channel = GrpcChannel.ForAddress(LocalHttpsHost);
            var client = new Greet.Greeter.GreeterClient(channel);
            var goodByeReply = await client.GoodByeAsync(request);
        //Then
            Assert.Equal($"Good bye {goodByeReply.Name}", goodByeReply.Message);
        }
    }
}
