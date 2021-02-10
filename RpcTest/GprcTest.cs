using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Xunit;

namespace RpcTest
{
    public class GrpcTest
    {
        private const string LocalHttpsHost = "https://localhost:5001";

        private const string LocalHttpHost = "http://localhost:5000";

        private static readonly string Id = Guid.NewGuid().ToString();

        private const string UserName = "YIM";

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

        [Fact]
        public async Task AuthenticatorAuthenticateTest()
        {
        //Given
            var request = new Auth.Identity
            {
                Id = GrpcTest.Id,
                UserName = UserName,
                Role = new Auth.Role(),
            };
            request.Role.EditPermissions.AddRange(new int[] { 5, 7, 8, 2, 9 });
            request.Role.QueryPermissions.AddRange(new int[] { 8, 2, 3, 1, 4 });
        //When
            using var channel = GrpcChannel.ForAddress(LocalHttpsHost);
            var client = new Auth.Authenticatior.AuthenticatiorClient(channel);
            var authenticateResult = await client.AuthenticateAsync(request);
        //Then
            Assert.True(authenticateResult.Result);
        }

        /// <summary>
        /// <strong>.NET 5</strong>客户端可以直接使用<strong>HTTP</strong>的GRPC远程调用
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UnsafeGrpcTest()
        {
        //Given
            var request = new Greet.HelloRequest
            {
                Name = "YIM",
            };
        //When
            using var channel = GrpcChannel.ForAddress(LocalHttpHost);
            var client = new Greet.Greeter.GreeterClient(channel);
            var helloReply = await client.SayHelloAsync(request);
        //Then
            Assert.Equal("Hello " + request.Name, helloReply.Message);
        }
    }
}
