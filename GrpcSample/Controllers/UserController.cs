using System.Threading.Tasks;
using Api;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcSample.Controllers
{
    public class UserController : Api.User.UserBase
    {
        private readonly ILogger _logger;

        public UserController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType());
        }

        public override Task<LoginReply> SignIn(LoginRequest request, ServerCallContext context)
        {
            var userId = request.Id;
            var password = request.Password;

            _logger.LogInformation(userId);
            _logger.LogInformation(password);

            var loginReply = new LoginReply
            {
                Result = true,
                Message = "登录成功"
            };

            return Task.FromResult(loginReply);
        }
    }
}