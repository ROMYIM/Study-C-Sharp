using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greet;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcSample.RemoteServices
{
    public class GreeterService : Greet.Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override Task<GoodByeReply> GoodBye(GoodByeRequest request, Grpc.Core.ServerCallContext context)
        {
            var reply = new GoodByeReply()
            {
                Name = "yim"
            };

            _logger.LogInformation(request.Name);
            _logger.LogInformation(request.Message);

            var name = request.Name;
            if (string.IsNullOrWhiteSpace(name))
            {
                reply.Result = false;
                reply.Message = "I don't who you are";    
            }
            else
            {
                reply.Result = true;
                reply.Message = $"Good bye {request.Name}";
            }
            return Task.FromResult(reply);
        }

        
    }
}
