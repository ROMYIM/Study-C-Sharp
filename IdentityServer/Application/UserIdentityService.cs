using Microsoft.AspNetCore.Identity;
using Domain.Identity.Entities;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Concurrent;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Application
{
    public class UserIdentityService : IUserPasswordStore<User>, IUserClaimsPrincipalFactory<User>
    {
        private static readonly ConcurrentDictionary<string, User> _users = new();
        
        static UserIdentityService()
        {
            _users.TryAdd("yim", new User
            {
                Id = "yim",
                UserName = "YIM",
                Password = "123456",
                Role = new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "荣儿",
                    QueryPermissions = new System.Collections.BitArray(new bool[] 
                    {
                        true, false, false, false, false, true, true, true, false, true
                    }),
                    EditPermissions = new System.Collections.BitArray(new bool[] 
                    {
                        true, false, false, false, false, true, true, true, false, true
                    }),
                }
            });
        }

        private readonly IPasswordHasher<User> _passwordHasher;

        private readonly ILogger _logger;

        public UserIdentityService(
            IPasswordHasher<User> passwordHasher,
            ILoggerFactory loggerFactory)
        {
            _passwordHasher = passwordHasher;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        public void Dispose()
        {
            
        }

        Task<IdentityResult> IUserStore<User>.CreateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ClaimsPrincipal> IUserClaimsPrincipalFactory<User>.CreateAsync(User user)
        {
            var claims =new List<Claim>()
            {
                new Claim(IdentityModel.JwtClaimTypes.Role,"需求"),
                new Claim(IdentityModel.JwtClaimTypes.NickName,"zikey"),
                new Claim(ClaimTypes.Name,"紫琪"),
                new Claim("eMail","57265177@qq.com"),
            };
            var identity = new ClaimsIdentity(claims);
            return Task.FromResult(new ClaimsPrincipal(identity));
        }

        Task<IdentityResult> IUserStore<User>.DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        

        Task<User> IUserStore<User>.FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (_users.TryGetValue(userId, out var user))
                return Task.FromResult(user);
            else
                return Task.FromCanceled<User>(cancellationToken);

        }

        Task<User> IUserStore<User>.FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            User userResult = null;
            foreach (var userItem in _users)
            {
                var user = userItem.Value;
                if (user.UserName == normalizedUserName)
                {
                    return Task.FromResult(user);
                }
            }
            return Task.FromResult(userResult);
        }

        Task<string> IUserStore<User>.GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        Task<string> IUserPasswordStore<User>.GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasher.HashPassword(user, user.Password);
            return Task.FromResult(hashedPassword);
        }

        Task<string> IUserStore<User>.GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        Task<string> IUserStore<User>.GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        Task<bool> IUserPasswordStore<User>.HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(user?.Password))
                return Task.FromResult(false);
            return Task.FromResult(true);
        }

        Task IUserStore<User>.SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            var user1 = _users[user.Id];
            user1.UserName = user.UserName;
            _users[user.Id] = user1;
            return Task.CompletedTask;
        }

        async Task IUserPasswordStore<User>.SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            var user_1 = await ((IUserStore<User>)this).FindByIdAsync(user.Id, cancellationToken);
            user_1.Password = passwordHash;
            _users[user.Id] = user_1;
        }

        async Task IUserStore<User>.SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            var user_1 = await ((IUserStore<User>)this).FindByIdAsync(user.Id, cancellationToken);
            user_1.UserName = userName;
            _users[user.Id] = user_1;
        }

        Task<IdentityResult> IUserStore<User>.UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        
    }
}