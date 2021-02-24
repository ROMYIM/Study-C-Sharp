using System.Threading.Tasks;
using System.Text;
using Auth;
using Common;
using Domain.Identity.Entities;
using Domain.Identity.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcSample.Controllers
{
    public class AuthenticationController : Auth.Authenticatior.AuthenticatiorBase
    {
        private readonly ILogger _logger;

        private readonly UserRepository _userRepository;

        public AuthenticationController(ILoggerFactory loggerFactory, UserRepository userRepository)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _userRepository = userRepository;
        }

        public override Task<Response> Authenticate(Identity request, ServerCallContext context)
        {
            var reponse = new Response();
            reponse.Result = true;
            var messageBuilder = new StringBuilder();

            var user = _userRepository.GetUser(request.Id);
            var editPermissions = user.Role.EditPermissions;
            var queryPermissions = user.Role.QueryPermissions;

            foreach (var permission in request.Role.EditPermissions)
            {
                if (!editPermissions[permission])
                {
                    reponse.Result = false;
                    messageBuilder.Append($"没有资源{permission}的编辑权限");
                    break;
                }
            }

            foreach (var permission in request.Role.QueryPermissions)
            {
                if (!queryPermissions[permission])
                {
                    reponse.Result = false;
                    messageBuilder.Append($"没有资源{permission}的查询权限");
                    break;
                }
            }

            reponse.Message = messageBuilder.ToString();
            return Task.FromResult(reponse);
        }
    }
}