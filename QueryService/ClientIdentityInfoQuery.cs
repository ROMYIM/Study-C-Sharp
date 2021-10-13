using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Infrastructure.Db;
using IdentityServer4;

namespace QueryService
{
    public class ClientIdentityInfoQuery : IClientStore
    {
        private readonly IEnumerable<Client> _clients = new []
        {
            /// <summary>
            ///  客户端模式
            /// </summary>
            /// <value></value>
            new Client
            {
                ClientId = "middleware-sample",//客户端惟一标识
                ClientName = "middleware",
                ClientSecrets = new [] { new Secret("middleware".Sha256()) },//客户端密码，进行了加密
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                //授权方式，客户端认证，只要ClientId+ClientSecrets
                AllowedScopes = new [] { "MiddleWare" },//允许访问的资源
                Claims=new List<ClientClaim>(){
                    new ClientClaim(IdentityModel.JwtClaimTypes.Role,"Admin"),
                    new ClientClaim(IdentityModel.JwtClaimTypes.NickName,"middleware"),
                    new ClientClaim("eMail","57265177@qq.com")
                }
            },

            /// <summary>
            /// 授权码模式
            /// </summary>
            /// <value></value>
            new Client
            {
                ClientId = "grpc-sample",//客户端惟一标识
                ClientName="grpc client",
                ClientSecrets = new [] { new Secret("grpc".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,//授权码
                RedirectUris={"http://localhost:5726/Ids4/IndexCodeToken" },//可以多个
                AllowedScopes = new [] { "grpc","TestApi" },//允许访问的资源
                AllowAccessTokensViaBrowser=true//允许将token通过浏览器传递
            },

            /// <summary>
            /// 用户名密码模式
            /// </summary>
            /// <value></value>
            new Client
            {
                ClientId = "netty-demo",//客户端惟一标识
                ClientSecrets = new [] { new Secret("eleven123456".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,//密码模式
                AllowedScopes = new [] { "UserApi","TestApi" },//允许访问的资源
                //cliam无效
            },

            /// <summary>
            /// 隐藏模式
            /// </summary>
            /// <value></value>
            new Client
            {
                ClientId = "netty-client",//客户端惟一标识
                ClientName="ApiClient for Implicit",
                ClientSecrets = new [] { new Secret("eleven123456".Sha256()) },
                AllowedGrantTypes = GrantTypes.Implicit,//隐藏模式
                RedirectUris={"http://localhost:5726/Ids4/IndexToken" },//可以多个，根据请求来的转发
                AllowedScopes = new [] { "UserApi","TestApi" },//允许访问的资源
                AllowAccessTokensViaBrowser=true//允许将token通过浏览器传递
            },

            /// <summary>
            /// 混合模式
            /// </summary>
            /// <value></value>
            new Client
            {
                AlwaysIncludeUserClaimsInIdToken=true,
                AllowOfflineAccess = true,

                ClientId = "Zhaoxi.AspNetCore31.AuthDemo",//客户端惟一标识
                ClientName="ApiClient for HyBrid",
                ClientSecrets = new [] { new Secret("eleven123456".Sha256()) },
                AccessTokenLifetime=3600,//默认1小时
                AllowedGrantTypes = GrantTypes.Hybrid,//混合模式
                RedirectUris={"http://localhost:5726/Ids4/IndexCodeToken" },//可以多个
                AllowedScopes = new [] {
                    "UserApi",
                    "TestApi",
                    IdentityServerConstants.StandardScopes.OpenId,//Ids4：获取Id_token，必需加入"openid"
                        IdentityServerConstants.StandardScopes.Profile,
                    "CustomIdentityResource"},//允许访问的资源
                AllowAccessTokensViaBrowser=true//允许将token通过浏览器传递
            }

        };

        public ClientIdentityInfoQuery()
        {
            
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(_clients.FirstOrDefault(c => c.ClientId == clientId));
        }

        public IEnumerable<Client> Clients => _clients;
    }
}
