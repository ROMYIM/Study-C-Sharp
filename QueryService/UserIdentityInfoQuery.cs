using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace QueryService
{
    public class UserIdentityInfoQuery
    {
        private readonly List<TestUser> _users = new()
        {
            new TestUser()
            {
                Username="RomYim",
                Password="yim123456",
                SubjectId="0",
                Claims = new List<Claim>()
                {
                    new Claim(IdentityModel.JwtClaimTypes.Role,"yim"),
                    new Claim(IdentityModel.JwtClaimTypes.NickName,"yim"),
                    new Claim("eMail","57265177@qq.com")
                }
            },

            new TestUser()
            {
                Username="Zikey",
                Password="zky123456",
                SubjectId="0",


                Claims=new List<Claim>()
                {
                    new Claim(IdentityModel.JwtClaimTypes.Role,"zikey"),
                    new Claim(IdentityModel.JwtClaimTypes.NickName,"zikey"),
                    new Claim(ClaimTypes.Name,"zikey"),
                    new Claim("eMail","57265177@qq.com"),
                    new Claim("prog","正式项目"),
                    new Claim("phonemodel","huawei"),
                    new Claim("phoneprise","5000元"),
                }
            }
        };

        public IEnumerable<TestUser> Users => _users;
    }
}