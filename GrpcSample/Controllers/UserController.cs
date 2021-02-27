using System.Threading.Tasks;
using Api;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Domain.Identity.Repositories;
using Domain.Identity.Entities;

namespace GrpcSample.Controllers
{
    public class UserController : Api.User.UserBase
    {
        private readonly ILogger _logger;

        private readonly UserRepository _userRepository;

        public UserController(ILoggerFactory loggerFactory, UserRepository userRepository)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _userRepository = userRepository;
        }

        public override Task<LoginReply> SignIn(LoginRequest request, ServerCallContext context)
        {
            var userId = request.Id;
            var password = request.Password;

            _logger.LogInformation(userId);
            _logger.LogInformation(password);

            var user = _userRepository.GetUser(userId);

            var httpContext = context.GetHttpContext();
            
            

            var loginReply = new LoginReply
            {
                Result = true,
                Message = "登录成功"
            };

            return Task.FromResult(loginReply);
        }
    }
}