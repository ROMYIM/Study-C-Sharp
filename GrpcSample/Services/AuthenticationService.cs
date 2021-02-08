using System.Threading.Tasks;
using Auth;
using Common;
using Domain.Identity.Entities;
using Domain.Identity.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcSample.RemoteServices
{
    public class AuthenticationService : Auth.Authenticatior.AuthenticatiorBase
    {
        private readonly ILogger _logger;

        public AuthenticationService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType());
        }

        public override Task<Response> Authenticate(Identity request, ServerCallContext context)
        {
            var role = request.Role;
            return base.Authenticate(request, context);
        }
    }
}