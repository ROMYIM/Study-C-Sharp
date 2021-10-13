using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace QueryService
{
    public class ResourceQuery : IResourceStore
    {
        private readonly List<ApiResource> _apiResources = new()
        {
            new ApiResource("UserApi", "用户获取API", new List<string>()
            {
                IdentityModel.JwtClaimTypes.Role,
                "eMail" 
            }),
            new ApiResource("TestApi", "用户TestAPI", new List<string>()
            {
                IdentityModel.JwtClaimTypes.Role,
                "eMail" 
            }),
            new ApiResource("MiddleWare", "中间件TestAPI", new List<string>()
            {
                "MiddleWare"
            })
        };

        private readonly List<ApiScope> _apiScopes = new()
        {
            new ApiScope("UserApi", "用户获取API", new List<string>()
            {
                IdentityModel.JwtClaimTypes.Role,
                "eMail" 
            }),
            new ApiScope("TestApi", "用户TestAPI", new List<string>()
            {
                IdentityModel.JwtClaimTypes.Role,
                "eMail" 
            }),
            new ApiScope("MiddleWare", "中间件TestAPI", new List<string>()
            {
                "MiddleWare"
            })
        };

        private readonly List<IdentityResource> _identityResources = new()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Phone(),
            new IdentityResources.Profile(),
            new IdentityResources.Address()

        };

        public ResourceQuery()
        {
            var middlewareApiSource = _apiResources.First(a => a.Name == "MiddleWare");
            middlewareApiSource.Scopes.Add("MiddleWare");
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            IEnumerable<ApiResource> result = _apiResources.Where(a => apiResourceNames.Contains(a.Name)).ToList();
            return Task.FromResult(result);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var result = new List<ApiResource>(_apiResources.Count);
            foreach (var source in _apiResources)
            {
                foreach (var name in scopeNames)
                {
                    if (source.Scopes.Contains(name))
                    {
                        result.Add(source);
                        break;
                    }
                }
            }

            return Task.FromResult(result.AsEnumerable());
        }

        public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var result = new List<ApiScope>(scopeNames.Count());
            foreach (var scope in _apiScopes)
            {
                foreach (var name in scopeNames)
                {
                    if (scope.Name == name)
                    {
                        result.Add(scope);
                        break;
                    }
                }
            }
            return Task.FromResult(result.AsEnumerable());
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            IEnumerable<IdentityResource> resources = _identityResources;
            return Task.FromResult(resources);
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            var resources = new Resources()
            {
                ApiResources = _apiResources,
                IdentityResources = _identityResources
            };
            return Task.FromResult(resources);
        }
    }
}